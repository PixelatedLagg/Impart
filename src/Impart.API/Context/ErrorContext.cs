using System.Text;
using System.Net.Sockets;

namespace Impart.API
{
    public class ErrorContext : Context
    {
        private Socket socket;
        public ErrorType Type
        {
            get
            {
                return type;
            }
        }
        private ErrorType type;

        public ErrorContext(Socket socket, ErrorType type)
        {
            this.socket = socket;
            this.type = type;
        }
        public void Respond(Json response)
        {
            string str = response.Render();
            byte[] bytes = Encoding.ASCII.GetBytes($"HTTP/1.1 {((int)type)}\r\nServer: cx1193719-b\r\nContent-Type: text/json\r\nAccept-Ranges: bytes\r\nContent-Length: {str.Length} \r\n\r\n{str}");
            try  
            {
                socket.Send(bytes, bytes.Length, 0);
            }
            catch
            {
                throw new ImpartError("Error in sending packets.");
            }
        }
    }
}