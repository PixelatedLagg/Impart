using System;
using System.Net;
using System.Text;
using System.Threading;
using System.Net.Sockets;

namespace Impart.API
{
    public class Soap : API
    {
        public Action<APIEventArgs, SoapContext> OnRequest;
        private int port;
        private TcpListener listener;
        private Thread thread;
        
        public Soap(int port = 7070)
        {
            this.port = port;
        }
        public void Start()
        {
            listener = new TcpListener(Dns.GetHostAddresses("localhost")[0], port);
            listener.Start();
            thread = new Thread(new ThreadStart(StartListen));
            thread.Start();
            Console.BackgroundColor = ConsoleColor.DarkMagenta;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine($" SOAP API hosted on localhost:{port} ");
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Gray;
        }
        private void StartListen()
        {
            while (true)
            {
                Socket mySocket = listener.AcceptSocket();
                if (mySocket.Connected)  
                {
                    Byte[] bReceive = new Byte[1024];
                    mySocket.Receive(bReceive, bReceive.Length, 0);
                    string sBuffer = Encoding.ASCII.GetString(bReceive);
                    Request request;
                    switch (sBuffer.Split(" ")[0])
                    {
                        case "GET":
                            request = Request.Get;
                            break;
                        case "PUSH":
                            request = Request.Push;
                            break;
                        case "PUT":
                            request = Request.Put;
                            break;
                        case "DELETE":
                            request = Request.Delete;
                            break;
                        case "HEAD":
                            request = Request.Head;
                            break;
                        case "CONNECT":
                            request = Request.Connect;
                            break;
                        case "OPTIONS":
                            request = Request.Options;
                            break;
                        case "TRACE":
                            request = Request.Trace;
                            break;
                        case "PATCH":
                            request = Request.Patch;
                            break;
                        default:
                            request = Request.Get;
                            break;
                    }
                    OnRequest?.Invoke(new APIEventArgs(request), new SoapContext(mySocket));
                }
            }
        }
    }
}