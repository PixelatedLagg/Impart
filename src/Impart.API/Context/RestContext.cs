using System.Text;
using Impart.Format;
using System.Net.Sockets;

namespace Impart.Api
{
    public class RestContext : Context
    {
        private Socket socket;
        public RestContext(Socket socket)
        {
            this.socket = socket;
        }
        public void Respond(JsonObject response)
        {
            string str = response.ToString();
            byte[] bytes = Encoding.ASCII.GetBytes($"HTTP/1.1 200 OK\r\nServer: cx1193719-b\r\nContent-Type: text/json\r\nAccept-Ranges: bytes\r\nContent-Length: {str.Length} \r\n\r\n{str}");
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