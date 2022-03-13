using System.Text;
using System.Net.Sockets;

namespace Impart.API
{
    public class SoapContext : Context
    {
        private Socket socket;
        public SoapContext(Socket socket)
        {
            this.socket = socket;
        }
        public void Respond(Xml response)
        {
            string str = response.Render();
            byte[] bytes = Encoding.ASCII.GetBytes($"HTTP/1.1 200 OK\r\nServer: cx1193719-b\r\nContent-Type: text/xml\r\nAccept-Ranges: bytes\r\nContent-Length: {str.Length} \r\n\r\n{str}");
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