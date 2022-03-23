using System;
using System.Net;
using System.Linq;
using System.Text;
using System.Threading;
using System.Reflection;
using System.Net.Sockets;
using System.Collections.Generic;

namespace Impart.API
{
    public class Rest : API
    {
        private Dictionary<string, MethodInfo> routes;
        public Action<APIEventArgs, ErrorContext> ErrorPage;
        private int port;
        private TcpListener listener;
        private Thread thread;
        
        public Rest(int port = 8080)
        {
            this.port = port;
        }
        public void Start()
        {
            routes = Assembly.GetExecutingAssembly().GetTypes().SelectMany(x => x.GetMethods())
                .Where(y => y.GetCustomAttributes().OfType<RestRequest>().Any())
                .ToDictionary(z => z.GetCustomAttribute<RestRequest>().Route);
            listener = new TcpListener(Dns.GetHostAddresses("localhost")[0], port);
            listener.Start();
            thread = new Thread(new ThreadStart(StartListen));
            thread.Start();
            Console.BackgroundColor = ConsoleColor.Blue;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine($" REST API hosted on localhost:{port} ");
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
                    Console.WriteLine(sBuffer);
                    string[] contents = sBuffer.Split(' ');
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
                    if (contents[1] == "/" && routes.ContainsKey("*"))
                    {
                        routes["*"].Invoke(Activator.CreateInstance(routes["*"].DeclaringType), new object[] {new APIEventArgs(request), new RestContext(mySocket)});
                    }
                    if (!routes.ContainsKey(contents[1]))
                    {
                        ErrorPage?.Invoke(new APIEventArgs(request), new ErrorContext(mySocket, ErrorType.NotFound));
                    }
                    else
                    {
                        routes[contents[1]].Invoke(Activator.CreateInstance(routes[contents[1]].DeclaringType), new object[] {new APIEventArgs(request), new RestContext(mySocket)});
                    }
                }
            }
        }
        protected void Stop()
        {
            listener.Stop();
        }

        [AttributeUsage(AttributeTargets.Method)]
        protected class RestRequest : System.Attribute
        {
            public RestRequest(string Route)
            {
                this.Route = Route;
            }
            public string Route {get; set;}
        }
    }
}