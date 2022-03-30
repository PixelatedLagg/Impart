using System.Text;
using Impart.Format;
using System.Net.Sockets;

namespace Impart.Api
{
    /// <summary>Information about an error from an API.</summary>
    public class ErrorContext : Context
    {
        /// <value>The error type.</value>
        public ErrorType Type
        {
            get
            {
                return _Type;
            }
        }
        private ErrorType _Type;
        private Socket Socket;

        /// <summary>Creates an ErrorContext instance with <paramref name="socket"/> as the socket, and <paramref name="type"/> as the error type.</summary>
        /// <returns>An ErrorContext instance.</returns>
        /// <param name="socket">The socket handling the API.</param>
        /// <param name="type">The error type.</param>
        public ErrorContext(Socket socket, ErrorType type)
        {
            Socket = socket;
            _Type = type;
        }

        /// <summary>Respond to the context with <paramref name="response"/> as the response.</summary>
        /// <param name="response">The response.</param>
        public void Respond(JsonObject response)
        {
            string str = response.ToString();
            byte[] bytes = Encoding.ASCII.GetBytes($"HTTP/1.1 {((int)_Type)}\r\nServer: cx1193719-b\r\nContent-Type: text/json\r\nAccept-Ranges: bytes\r\nContent-Length: {str.Length} \r\n\r\n{str}");
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