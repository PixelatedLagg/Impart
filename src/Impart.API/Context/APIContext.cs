using Impart;
using System.Net;
using System.Text;
using System.Threading;
using System.Net.Sockets;

namespace Impart.API
{
    public class APIContext
    {
        private Socket socket;
        public APIContext(Socket socket)
        {
            this.socket = socket;
        }
        public void Respond(string response)
        {
            byte[] bytes = Encoding.ASCII.GetBytes($"HTTP/1.1 200 OK\r\nServer: cx1193719-b\r\nContent-Type: text/html\r\nAccept-Ranges: bytes\r\nContent-Length: {response.Length} \r\n\r\n{response}");
            try  
            {
                socket.Send(bytes, bytes.Length, 0);
            }
            catch
            {
                throw new ImpartError("Error in sending packets.");
            }
        }
        public void Respond(Json response)
        {
            string str = response.Render();
            byte[] bytes = Encoding.ASCII.GetBytes($"HTTP/1.1 200 OK\r\nServer: cx1193719-b\r\nContent-Type: text/html\r\nAccept-Ranges: bytes\r\nContent-Length: {str.Length} \r\n\r\n{str}");
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