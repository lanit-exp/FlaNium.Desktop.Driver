using System;
using System.Globalization;
using System.IO;
using System.Net;
using System.Net.Sockets;
using FlaNium.Desktop.Driver.Common;

namespace FlaNium.Desktop.Driver {

    public class Listener {

        private static string _urnPrefix;


        private UriDispatchTables dispatcher;

        private CommandExecutorDispatchTable executorDispatcher;

        private TcpListener listener;


        public Listener(int listenerPort) {
            this.Port = listenerPort;
        }


        public static string UrnPrefix {
            get => _urnPrefix;

            set {
                if (!string.IsNullOrEmpty(value)) {
                    // Normalize prefix
                    _urnPrefix = "/" + value.Trim('/');
                }
            }
        }

        public int Port { get; private set; }

        public Uri Prefix { get; private set; }


        public void StartListening() {
            try {
                this.listener = new TcpListener(IPAddress.Any, this.Port);

                this.Prefix = new Uri(string.Format(CultureInfo.InvariantCulture, "http://localhost:{0}", this.Port));
                this.dispatcher = new UriDispatchTables(new Uri(this.Prefix, UrnPrefix));
                this.executorDispatcher = new CommandExecutorDispatchTable();

                // Start listening for client requests.
                this.listener.Start();

                // Enter the listening loop
                while (true) {
                    Logger.Debug("Waiting for a connection...");

                    // Perform a blocking call to accept requests. 
                    var client = this.listener.AcceptTcpClient();

                    // Get a stream object for reading and writing
                    using (var stream = client.GetStream()) {
                        var acceptedRequest = HttpRequest.ReadFromStreamWithoutClosing(stream);

                        if (string.IsNullOrWhiteSpace(acceptedRequest.StartingLine)) {
                            Logger.Warn("ACCEPTED EMPTY REQUEST");
                        }
                        else {
                            Logger.Debug("ACCEPTED REQUEST {0}", acceptedRequest.StartingLine);

                            var response = this.HandleRequest(acceptedRequest);
                            using (var writer = new StreamWriter(stream)) {
                                try {
                                    writer.Write(response);
                                    writer.Flush();
                                }
                                catch (IOException ex) {
                                    Logger.Error("Error occured while writing response: {0}", ex);
                                }
                            }
                        }

                        // Shutdown and end connection
                    }

                    client.Close();

                    Logger.Debug("Client closed\n");
                }
            }
            catch (SocketException ex) {
                Logger.Error("SocketException occurred while trying to start listner: {0}", ex);

                throw;
            }
            catch (ArgumentException ex) {
                Logger.Error("ArgumentException occurred while trying to start listner: {0}", ex);

                throw;
            }
            finally {
                // Stop listening for new clients.
                this.listener.Stop();
            }
        }

        public void StopListening() {
            this.listener.Stop();
        }


        private string HandleRequest(HttpRequest acceptedRequest) {
            var firstHeaderTokens = acceptedRequest.StartingLine.Split(' ');
            var method = firstHeaderTokens[0];
            var resourcePath = firstHeaderTokens[1];

            var uriToMatch = new Uri(this.Prefix, resourcePath);
            var matched = this.dispatcher.Match(method, uriToMatch);

            if (matched == null) {
                Logger.Warn("Unknown command recived: {0}", uriToMatch);

                return HttpResponseHelper.ResponseString(HttpStatusCode.NotFound, "Unknown command " + uriToMatch);
            }

            var commandName = matched.Data.ToString();
            var commandToExecute = new Command(commandName, acceptedRequest.MessageBody);
            foreach (string variableName in matched.BoundVariables.Keys) {
                commandToExecute.Parameters[variableName] = matched.BoundVariables[variableName];
            }

            var commandResponse = this.ProcessCommand(commandToExecute);

            return HttpResponseHelper.ResponseString(commandResponse.HttpStatusCode, commandResponse.Content);
        }

        private CommandResponse ProcessCommand(Command command) {
            Logger.Info("COMMAND {0}\r\n{1}", command.Name, command.Parameters.ToString());
            var executor = this.executorDispatcher.GetExecutor(command.Name);
            executor.ExecutedCommand = command;
            var respnose = executor.Do();

            // Имеется значительная задержка при выводе объемных логов (таких как скриншоты) в консоль
            if (command.Name.ToLower().Contains("screenshot")) {
                Logger.Debug("RESPONSE:\r\n Content length: {0}", respnose.Content.Length);
            }
            else {
                Logger.Debug("RESPONSE:\r\n{0}", respnose);
            }

            return respnose;
        }

    }

}