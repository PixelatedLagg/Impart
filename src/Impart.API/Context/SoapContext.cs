using System.Text;
using Impart.Format;
using System.Net.Sockets;

namespace Impart.Api
{
    /// <summary>Information about a SOAP context.</summary>
    public sealed class SoapContext
    {
        private Socket Socket;

        /// <summary>Creates an SoapContext instance with <paramref name="socket"/> as the socket.</summary>
        /// <returns>An SoapContext instance.</returns>
        /// <param name="socket">The socket handling the API.</param>
        public SoapContext(Socket socket)
        {
            Socket = socket;
        }

        /// <summary>Respond to the context with <paramref name="response"/> as the response.</summary>
        /// <param name="response">The response.</param>
        public void Respond(string response) //TODO change response to Xml
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