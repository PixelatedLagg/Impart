using System.Text;
using System.Net.Sockets;

namespace Impart.Api
{
    /// <summary>Information about a SoapContext.</summary>
    public sealed class SoapContext
    {
        private Socket Socket;

        /// <summary>Creates an SoapContext instance.</summary>
        /// <param name="socket">The Socket handling the API.</param>
        public SoapContext(Socket socket)
        {
            Socket = socket;
        }

        /// <summary>Respond to the context.</summary>
        /// <param name="response">The response.</param>
        public void Respond(string response)
        {
            string str = response;
            byte[] bytes = Encoding.ASCII.GetBytes($"HTTP/1.1 200 OK\r\nServer: cx1193719-b\r\nContent-Type: text/xml\r\nAccept-Ranges: bytes\r\nContent-Length: {str.Length} \r\n\r\n{str}");
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