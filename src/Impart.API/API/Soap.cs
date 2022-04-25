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
        private int _Port;

        /// <value>The value of the localhost port the API is being hosted on.</value>
        public int Port
        {
            get
            {
                return _Port;
            }
        }

        /// <value>The method to be called in the event of an error.</value>
        public Action<APIEventArgs, ErrorContext> Error;

        private Dictionary<string, MethodInfo> _Routes;
        private TcpListener _Listener;
        private Thread _Thread;
        private Socket _Socket;
        
        /// <summary>Creates a Soap instance with <paramref name="port"/> as the port.</summary>
        /// <param name="port">The port to host on. (Default is 7070)</param>
        public Soap(int port = 7070)
        {
            _Port = port;
            _Routes = Assembly.GetExecutingAssembly().GetTypes().SelectMany(x => x.GetMethods()).Where(y => y.GetCustomAttributes().OfType<SoapRequest>().Any()).ToDictionary(z => z.GetCustomAttribute<SoapRequest>().Route);
        }

        /// <summary>Start the Soap API.</summary>
        public void Start()
        {
            try
            {
                _Listener = new TcpListener(Dns.GetHostAddresses("localhost")[0], Port);
                _Listener.Start();
                _Thread = new Thread(new ThreadStart(StartListen));
                _Thread.Start();
                Console.BackgroundColor = ConsoleColor.DarkMagenta;
                Console.ForegroundColor = ConsoleColor.Black;
                Console.WriteLine($" SOAP API hosted on localhost:{Port} ");
                Console.BackgroundColor = ConsoleColor.Black;
                Console.ForegroundColor = ConsoleColor.Gray;
            }
            catch
            {
                throw new ImpartError("Error in starting the API.");
            }
        }

        /// <summary>Stop the Soap API.</summary>
        public void Stop() => _Listener.Stop();

        /// <summary>Attribute to hook Soap routes to methods.</summary>
        [AttributeUsage(AttributeTargets.Method)]
        protected class SoapRequest : System.Attribute
        {
            /// <value>The Soap route.</value>
            public string Route { get; set; }

            /// <summary>Creates a SoapRequest instance.</summary>
            /// <param name="route">The route to hook the method to.</param>
            public SoapRequest(string route)
            {
                Route = route;
            }
        }
        private void StartListen()
        {
            while (true)
            {
                _Socket = _Listener.AcceptSocket();
                if (_Socket.Connected)  
                {
                    Byte[] bReceive = new Byte[1024];
                    _Socket.Receive(bReceive, bReceive.Length, 0);
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
                    if (contents[1] == "/" && _Routes.ContainsKey("*"))
                    {
                        _Routes["*"].Invoke(Activator.CreateInstance(_Routes["*"].DeclaringType), new object[] {new APIEventArgs(request), new SoapContext(_Socket)});
                    }
                    if (!_Routes.ContainsKey(contents[1]))
                    {
                        Error?.Invoke(new APIEventArgs(request), new ErrorContext(_Socket, ErrorType.NotFound));
                    }
                    else
                    {
                        _Routes[contents[1]].Invoke(Activator.CreateInstance(_Routes[contents[1]].DeclaringType), new object[] {new APIEventArgs(request), new SoapContext(_Socket)});
                    }
                }
            }
        }
    }
}