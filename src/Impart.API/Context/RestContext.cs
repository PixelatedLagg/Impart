using System.Text;
using System.Net.Sockets;

namespace Impart.Api
{
    /// <summary>Information about a RestContext.</summary>
    public sealed class RestContext
    {
        private Socket Socket;

        /// <summary>Creates an RestContext instance.</summary>
        /// <param name="socket">The Socket handling the API.</param>
        public RestContext(Socket socket)
        {
            Socket = socket;
        }

        /// <summary>Respond to the context.</summary>
        /// <param name="response">The JSON response.</param>
        public void Respond(string response)
        {
            string str = response;
            byte[] bytes = Encoding.ASCII.GetBytes($"HTTP/1.1 200 OK\r\nServer: cx1193719-b\r\nContent-Type: text/json\r\nAccept-Ranges: bytes\r\nContent-Length: {str.Length} \r\n\r\n{str}");
            try  
            {
                Socket.Send(bytes, bytes.Length, 0);
            }
            catch
            {
                throw new ImpartError("Error in sending packets.");
            }
        }
    }
}