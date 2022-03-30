using System;
using System.Net;
using System.Linq;
using System.Text;
using System.Threading;
using System.Reflection;
using System.Net.Sockets;
using System.Collections.Generic;

namespace Impart.Api
{
    /// <summary>The class for hosting a REST API.</summary>
    public class Rest : API
    {
        /// <value>The method to be called in the event of an error.</value>
        public Action<APIEventArgs, ErrorContext> Error;
        private Dictionary<string, MethodInfo> Routes;
        private int Port;
        private TcpListener Listener;
        private Thread Thread;
        
        /// <summary>Creates a Rest instance with <paramref name="port"/> as the port.</summary>
        /// <returns>A Rest instance.</returns>
        /// <param name="port">The port to host on. (Default is 8080)</param>
        public Rest(int port = 8080)
        {
            Port = port;
            Routes = Assembly.GetExecutingAssembly().GetTypes().SelectMany(x => x.GetMethods())
                .Where(y => y.GetCustomAttributes().OfType<RestRequest>().Any())
                .ToDictionary(z => z.GetCustomAttribute<RestRequest>().Route);
        }

        /// <summary>Start the Rest API.</summary>
        public void Start()
        {
            Listener = new TcpListener(Dns.GetHostAddresses("localhost")[0], Port);
            Listener.Start();
            Thread = new Thread(new ThreadStart(StartListen));
            Thread.Start();
            Console.BackgroundColor = ConsoleColor.Blue;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine($" REST API hosted on localhost:{Port} ");
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Gray;
        }

        /// <summary>Stop the Rest API.</summary>
        public void Stop()
        {
            Listener.Stop();
        }

        /// <summary>Attribute to hook Rest routes to methods.</summary>
        [AttributeUsage(AttributeTargets.Method)]
        protected class RestRequest : System.Attribute
        {
            /// <summary>Creates a RestRequest instance with <paramref name="route"/> as the route.</summary>
            /// <returns>A RestRequest instance.</returns>
            /// <param name="route">The route to hook the method to.</param>
            public RestRequest(string route)
            {
                Route = route;
            }

            /// <value>The Rest route.</value>
            public string Route {get; set;}
        }
        private void StartListen()
        {
            while (true)
            {
                Socket mySocket = Listener.AcceptSocket();
                if (mySocket.Connected)  
                {
                    Byte[] bReceive = new Byte[1024];
                    mySocket.Receive(bReceive, bReceive.Length, 0);
                    string sBuffer = Encoding.ASCII.GetString(bReceive);
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
                    if (contents[1] == "/" && Routes.ContainsKey("*"))
                    {
                        Routes["*"].Invoke(Activator.CreateInstance(Routes["*"].DeclaringType), new object[] {new APIEventArgs(request), new RestContext(mySocket)});
                    }
                    if (!Routes.ContainsKey(contents[1]))
                    {
                        Error?.Invoke(new APIEventArgs(request), new ErrorContext(mySocket, ErrorType.NotFound));
                    }
                    else
                    {
                        Routes[contents[1]].Invoke(Activator.CreateInstance(Routes[contents[1]].DeclaringType), new object[] {new APIEventArgs(request), new RestContext(mySocket)});
                    }
                }
            }
        }
    }
}