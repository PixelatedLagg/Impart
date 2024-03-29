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
    public class Rest
    {
        private int _Port;

        /// <summary>The value of the localhost port the API is being hosted on.</summary>
        public int Port
        {
            get
            {
                return _Port;
            }
        }

        /// <summary>The method to be called in the event of an error.</summary>
        public Action<APIEventArgs, ErrorContext> Error;

        private Dictionary<string, MethodInfo> _Routes;
        private TcpListener _Listener;
        private Thread _Thread;
        private Socket _Socket;
        
        /// <summary>Creates a Rest instance with <paramref name="port"/> as the port.</summary>
        /// <param name="port">The port to host on. (Default is 8080)</param>
        public Rest(int port = 8080)
        {
            _Port = port;
            _Routes = Assembly.GetExecutingAssembly().GetTypes().SelectMany(x => x.GetMethods()).Where(y => y.GetCustomAttributes().OfType<RestRequest>().Any()).ToDictionary(z => z.GetCustomAttribute<RestRequest>().Route);
        }

        /// <summary>Start the Rest API.</summary>
        public void Start()
        {
            try
            {
                _Listener = new TcpListener(Dns.GetHostAddresses("localhost")[0], _Port);
                _Listener.Start();
                _Thread = new Thread(new ThreadStart(StartListen));
                _Thread.Start();
                Console.BackgroundColor = ConsoleColor.Blue;
                Console.ForegroundColor = ConsoleColor.Black;
                Console.WriteLine($" REST API hosted on localhost:{_Port} ");
                Console.BackgroundColor = ConsoleColor.Black;
                Console.ForegroundColor = ConsoleColor.Gray;
            }
            catch
            {
                throw new ImpartError("Error in starting the API.");
            }
        }

        /// <summary>Stop the Rest API.</summary>
        public void Stop() => _Listener.Stop();

        /// <summary>Attribute to hook Rest routes to methods.</summary>
        [AttributeUsage(AttributeTargets.Method)]
        protected class RestRequest : System.Attribute
        {
            /// <summary>The Rest route.</summary>
            public string Route { get; set; }

            /// <summary>Creates a RestRequest instance.</summary>
            /// <param name="route">The route to hook the method to.</param>
            public RestRequest(string route)
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
                    request = sBuffer.Split(" ")[0] switch
                    {
                        "GET" => RequestType.Get,
                        "PUSH" => RequestType.Push,
                        "PUT" => RequestType.Put,
                        "DELETE" => RequestType.Delete,
                        "HEAD" => RequestType.Head,
                        "CONNECT" => RequestType.Connect,
                        "OPTIONS" => RequestType.Options,
                        "TRACE" => RequestType.Trace,
                        "PATCH" => RequestType.Patch,
                        _ => RequestType.Get
                    };
                    if (contents[1] == "/" && _Routes.ContainsKey("*"))
                    {
                        _Routes["*"].Invoke(Activator.CreateInstance(_Routes["*"].DeclaringType), new object[] {new APIEventArgs(request), new RestContext(_Socket)});
                    }
                    if (!_Routes.ContainsKey(contents[1]))
                    {
                        Error?.Invoke(new APIEventArgs(request), new ErrorContext(_Socket, ErrorType.NotFound));
                    }
                    else
                    {
                        _Routes[contents[1]].Invoke(Activator.CreateInstance(_Routes[contents[1]].DeclaringType), new object[] {new APIEventArgs(request), new RestContext(_Socket)});
                    }
                }
            }
        }
    }
}