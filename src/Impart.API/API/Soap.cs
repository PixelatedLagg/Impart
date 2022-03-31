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
    /// <summary>The class for hosting a SOAP API.</summary>
    public class Soap
    {
        /// <value>The method to be called in the event of an error.</value>
        public Action<APIEventArgs, ErrorContext> Error;
        private Dictionary<string, MethodInfo> Routes;
        private int Port;
        private TcpListener Listener;
        private Thread Thread;
        
        /// <summary>Creates a Soap instance with <paramref name="port"/> as the port.</summary>
        /// <returns>A Soap instance.</returns>
        /// <param name="port">The port to host on. (Default is 7070)</param>
        public Soap(int port = 7070)
        {
            Port = port;
            Routes = Assembly.GetExecutingAssembly().GetTypes().SelectMany(x => x.GetMethods())
                .Where(y => y.GetCustomAttributes().OfType<SoapRequest>().Any())
                .ToDictionary(z => z.GetCustomAttribute<SoapRequest>().Route);
        }

        /// <summary>Start the Soap API.</summary>
        public void Start()
        {
            Listener = new TcpListener(Dns.GetHostAddresses("localhost")[0], Port);
            Listener.Start();
            Thread = new Thread(new ThreadStart(StartListen));
            Thread.Start();
            Console.BackgroundColor = ConsoleColor.DarkMagenta;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine($" SOAP API hosted on localhost:{Port} ");
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Gray;
        }

        /// <summary>Stop the Soap API.</summary>
        public void Stop()
        {
            Listener.Stop();
        }

        /// <summary>Attribute to hook Soap routes to methods.</summary>
        [AttributeUsage(AttributeTargets.Method)]
        protected class SoapRequest : System.Attribute
        {
            /// <summary>Creates a SoapRequest instance with <paramref name="route"/> as the route.</summary>
            /// <returns>A SoapRequest instance.</returns>
            /// <param name="route">The route to hook the method to.</param>
            public SoapRequest(string route)
            {
                Route = route;
            }

            /// <value>The Soap route.</value>
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
                    RequestType request;
                    switch (sBuffer.Split(" ")[0])
                    {
                        case "GET":
                            request = RequestType.Get;
                            break;
                        case "PUSH":
                            request = RequestType.Push;
                            break;
                        case "PUT":
                            request = RequestType.Put;
                            break;
                        case "DELETE":
                            request = RequestType.Delete;
                            break;
                        case "HEAD":
                            request = RequestType.Head;
                            break;
                        case "CONNECT":
                            request = RequestType.Connect;
                            break;
                        case "OPTIONS":
                            request = RequestType.Options;
                            break;
                        case "TRACE":
                            request = RequestType.Trace;
                            break;
                        case "PATCH":
                            request = RequestType.Patch;
                            break;
                        default:
                            request = RequestType.Get;
                            break;
                    }
                    if (contents[1] == "/" && Routes.ContainsKey("*"))
                    {
                        Routes["*"].Invoke(Activator.CreateInstance(Routes["*"].DeclaringType), new object[] {new APIEventArgs(request), new SoapContext(mySocket)});
                    }
                    if (!Routes.ContainsKey(contents[1]))
                    {
                        Error?.Invoke(new APIEventArgs(request), new ErrorContext(mySocket, ErrorType.NotFound));
                    }
                    else
                    {
                        Routes[contents[1]].Invoke(Activator.CreateInstance(Routes[contents[1]].DeclaringType), new object[] {new APIEventArgs(request), new SoapContext(mySocket)});
                    }
                }
            }
        }
    }
}