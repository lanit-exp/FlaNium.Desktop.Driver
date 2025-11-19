using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using FlaNium.Desktop.Driver.Common;

namespace FlaNium.Desktop.Driver {

    public class Listener {

        private UriDispatchTables dispatcher;

        private CommandExecutorDispatchTable executorDispatcher;

        private TcpListener listener;

        private int Port { get; }
        private HashSet<IPAddress> allowedIps { get; }


        public Listener(int listenerPort, string allowedIps) {
            this.Port = listenerPort;
            this.allowedIps = GetSetOfIP(allowedIps);

            this.dispatcher = new UriDispatchTables();
            this.executorDispatcher = new CommandExecutorDispatchTable();
        }

        private static HashSet<IPAddress> GetSetOfIP(string allowedIps) {
            HashSet<IPAddress> ipSet = new HashSet<IPAddress>();
            ipSet.Add(IPAddress.Parse("127.0.0.1"));

            var ipArray = allowedIps.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

            foreach (var ip in ipArray) {
                var trimmedIp = ip.Trim();
                if (IPAddress.TryParse(trimmedIp, out var ipAddress)) {
                    ipSet.Add(ipAddress);
                }
                else {
                    Console.WriteLine($"    [ERROR] Invalid IP address: {trimmedIp}\n");
                }
            }

            return ipSet;
        }


        public void StartListening() {
            string ipList = string.Join(", ", allowedIps);

            Console.WriteLine($"    Starting Windows Desktop Driver on port '{Port}'\n");
            Console.WriteLine($"    Allowed IP addresses: {ipList}\n");

            try {
                listener = new TcpListener(IPAddress.Any, Port);

                listener.Start();

                while (true) {
                    Logger.Debug("Waiting for a connection...");

                    var client = listener.AcceptTcpClient();

                    HandleClient(client);
                    client.Close();
                    Logger.Debug("Client closed\n\n\n");
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
                listener?.Stop();
            }
        }


        private void HandleClient(TcpClient client) {
            using (NetworkStream stream = client.GetStream()) {
                HttpRequest acceptedRequest = HttpRequest.ReadFromStreamWithoutClosing(stream);

                IPAddress remoteAddress = (client.Client.RemoteEndPoint as IPEndPoint)?.Address;

                if (string.IsNullOrWhiteSpace(acceptedRequest.StartingLine)) {
                    Logger.Warn("ACCEPTED EMPTY REQUEST");
                }
                else {
                    Logger.Debug("ACCEPTED REQUEST {0}", acceptedRequest.StartingLine);

                    string response;

                    if (allowedIps.Contains(remoteAddress)) {
                        response = HandleRequest(acceptedRequest);
                    }
                    else {
                        string mes =
                            $"Access from IP '{remoteAddress}' is denied. " +
                            $"It is necessary to add IP address to the white list when starting the driver (parameter: --allowed-ips).";

                        CommandResponse commandResponse = CommandResponse.Create(new JsonResponse(ResponseStatus.UnknownError, mes));
                        response = HttpResponseHelper.ResponseString(commandResponse.HttpStatusCode,
                            commandResponse.Content);
                        Logger.Error(mes);
                    }

                    using (StreamWriter writer = new StreamWriter(stream)) {
                        try {
                            writer.Write(response);
                            writer.Flush();
                        }
                        catch (IOException ex) {
                            Logger.Error("Error occured while writing response: {0}", ex);
                        }
                    }
                }
            }
        }

        private string HandleRequest(HttpRequest acceptedRequest) {
            var firstHeaderTokens = acceptedRequest.StartingLine.Split(' ');
            var method = firstHeaderTokens[0];
            var uriToMatch = firstHeaderTokens[1];

            var matched = dispatcher.Match(method, uriToMatch);

            if (matched == null) {
                Logger.Warn("Unknown command recived: {0}", uriToMatch);

                return HttpResponseHelper.ResponseString(HttpStatusCode.NotFound, "Unknown command " + uriToMatch);
            }

            var commandName = matched.Data.ToString();
            var commandToExecute = new Command(commandName, acceptedRequest.MessageBody);

            foreach (string variableName in matched.BoundVariables.Keys) {
                commandToExecute.Parameters[variableName] = matched.BoundVariables[variableName];
            }

            var commandResponse = ProcessCommand(commandToExecute);

            return HttpResponseHelper.ResponseString(commandResponse.HttpStatusCode, commandResponse.Content);
        }

        private CommandResponse ProcessCommand(Command command) {
            Logger.Info("COMMAND {0}\r\n{1}", command.Name, command.GetParametersAsString());

            var executor = executorDispatcher.GetExecutor(command.Name);
            executor.ExecutedCommand = command;
            var response = executor.Do();

            Logger.Debug("RESPONSE:\r\n{0}", GetResponseLog(command.Name, response));

            return response;
        }

        private string GetResponseLog(string commandName, CommandResponse response) {
            string[] ignoreCommands = { "screenshot", "filedownload" };

            if (ignoreCommands.Any(s => commandName.ToLower().Contains(s)) &&
                response.HttpStatusCode == HttpStatusCode.OK) {
                return
                    "░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░\n" +
                    "░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░\n" +
                    $" Content length: {response.Content.Length}\n" +
                    "░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░\n" +
                    "░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░\n";
            }

            return response.ToString();
        }

    }

}