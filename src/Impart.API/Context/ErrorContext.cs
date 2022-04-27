using System.Text;
using Impart.Format;
using System.Net.Sockets;

namespace Impart.Api
{
    /// <summary>Information about an error from an API.</summary>
    public sealed class ErrorContext
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

        /// <summary>Creates an ErrorContext instance.</summary>
        /// <param name="socket">The Socket handling the API.</param>
        /// <param name="type">The ErrorType.</param>
        public ErrorContext(Socket socket, ErrorType type)
        {
            Socket = socket;
            _Type = type;
        }

        /// <summary>Respond to the context.</summary>
        /// <param name="response">The JSON response.</param>
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
        
        /// <summary>Respond to the context.</summary>
        /// <param name="response">The XML response.</param>
        public void Respond(XmlObject response)
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