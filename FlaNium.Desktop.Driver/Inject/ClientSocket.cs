using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace FlaNium.Desktop.Driver.Inject {

    class ClientSocket {

        private Socket socket;
        private IPEndPoint ipPoint;

        public ClientSocket(int port = 8889, string address = "127.0.0.1") {
            ipPoint = new IPEndPoint(IPAddress.Parse(address), port);

            socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            socket.Connect(ipPoint);

            Logger.Debug(ipPoint + " - connection was SUCCESSFUL!");
        }

        public string DataExchange(string message) {
            Logger.Debug("[REQUEST  to   {0}]: {1}", ipPoint.ToString(), message);

            Send(message);

            string answer = Receive();

            Logger.Debug("[RESPONSE from {0}]: {1}", ipPoint.ToString(), answer);

            return answer;
        }

        public IDictionary<string, JToken> DataExchange(IDictionary<string, JToken> map) {
            string request = JsonConvert.SerializeObject(map);
            string response = DataExchange(request);

            return JsonConvert.DeserializeObject<IDictionary<string, JToken>>(response);
        }


        private void Send(string message) {
            byte[] data = Encoding.Unicode.GetBytes(message);
            socket.Send(data);
        }

        private string Receive() {
            byte[] data = new byte[4096];

            StringBuilder builder = new StringBuilder();

            int bytes;
            do {
                bytes = socket.Receive(data, data.Length, 0);
                builder.Append(Encoding.Unicode.GetString(data, 0, bytes));
            } while (socket.Available > 0);

            var regex = new Regex(@"[\p{Cc}\p{Cf}\p{Mn}\p{Me}\p{Zl}\p{Zp}]");

            return regex.Replace(builder.ToString(), "");
        }

        public void FreeSocket() {
            try {
                socket.Shutdown(SocketShutdown.Both);
            }
            catch (Exception ex) {
                Logger.Error(ex.Message);
            }
            finally {
                socket.Close();
                Logger.Debug(ipPoint + " - connection CLOSED!");
            }
        }

    }

}