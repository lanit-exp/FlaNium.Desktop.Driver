using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;

namespace FlaNium.Desktop.Driver {

    public class HttpRequest {

        public Dictionary<string, string> Headers { get; set; }

        public string MessageBody { get; private set; }

        public string StartingLine { get; private set; }


        public static HttpRequest ReadFromStreamWithoutClosing(Stream stream) {
            var request = new HttpRequest();
            var streamReader = new StreamReader(stream);

            request.StartingLine = streamReader.ReadLine();

            request.Headers = ReadHeaders(streamReader);

            var contentLength = GetContentLength(request.Headers);
            request.MessageBody = contentLength != 0 ? ReadContent(streamReader, contentLength) : string.Empty;

            return request;
        }


        private static int GetContentLength(IReadOnlyDictionary<string, string> headers) {
            var contentLength = 0;
            string contentLengthString;
            if (headers.TryGetValue("Content-Length", out contentLengthString)) {
                contentLength = Convert.ToInt32(contentLengthString, CultureInfo.InvariantCulture);
            }

            return contentLength;
        }

        
        private static string ReadContent(TextReader textReader, int contentLength) {
            var sb = new StringBuilder(contentLength);
            var buffer = new char[4096];
            int totalBytesRead = 0;
    
            while (totalBytesRead < contentLength)
            {
                int bytesToRead = Math.Min(buffer.Length, contentLength - totalBytesRead);
                int bytesRead = textReader.Read(buffer, 0, bytesToRead);
        
                if (bytesRead == 0) break;
        
                sb.Append(buffer, 0, bytesRead);
                totalBytesRead += bytesRead;
            }
    
            return sb.ToString();
        }

        private static Dictionary<string, string> ReadHeaders(TextReader textReader) {
            var headers = new Dictionary<string, string>();
            string header;
            while (!string.IsNullOrEmpty(header = textReader.ReadLine())) {
                var splitHeader = header.Split(':');
                headers.Add(splitHeader[0], splitHeader[1].Trim(' '));
            }

            return headers;
        }

    }

}