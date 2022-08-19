using System;
using System.Net;
using System.Text;
using System.Threading;
using System.Net.Sockets;
using System.Collections.Generic;

namespace Impart
{
    /// <summary>Host WebPages.</summary>
    public sealed class Website
    {
        private bool _Running;

        /// <summary>Whether the Website is currently running. Default is false.</summary>
        public bool Running
        {
            get
            {
                return _Running;
            }
        }

        /// <summary>A Dictionary of all the WebPages included in the Website. Each key is a URL path, while each value is a WebPage accessed via that path. To set a 404 WebPage, simply name the path "404".</summary>
        public Dictionary<string, WebPage> Pages = new Dictionary<string, WebPage>();

        /// <summary>The underlying TCP listener the Website uses. Do not manually start or stop as the listener thread depends on the Website methods to do so.</summary>
        public TcpListener Listener;

        /// <summary>Called when a client requests a WebPage.</summary>
        public Action<WebsiteRequestArgs> OnRequest;

        /// <summary>Called just before the TCP listener begins listening in a new thread.</summary>
        public Action OnListener;

        private TcpListener _Listener;
        private WebPage _ErrorPage;
        private Thread _Thread;
        private Socket _Socket;
        private int _Port;

        /// <summary>Creates a Website instance.</summary>
        /// <param name="page">The default WebPage.</param>
        /// <param name="port">The local port to use. (Default is 5050)</param>
        public Website(WebPage page, int port = 5050)
        {
            Pages.Add("", page);
            _Port = port;
            _ErrorPage = null;
        }

        /// <summary>Start the Website.</summary>
        public void Start()
        {
            try  
            {
                _Running = true;
                _Listener = new TcpListener(Dns.GetHostAddresses("localhost")[0], _Port);
                OnListener?.Invoke();
                _Listener.Start();
                _Thread = new Thread(new ThreadStart(StartListen));
                _Thread.Start();
                Logger.Info($"Website hosted on localhost:{_Port}");
            }
            catch
            {
                throw new ImpartError("Error in starting the Website.");
            }
        }

        /// <summary>Stop the Website. This will also close the TCP listener thread.</summary>
        public void Stop()
        {
            _Listener.Stop();
            _Running = false;
        }
        private void StartListen()
        {
            while (_Running)
            {
                _Socket = _Listener.AcceptSocket();
                if (_Socket.Connected)
                {
                    #if LOGGING
                    Logger.Info($"Client connected on IP address: {IPAddress.Parse(((IPEndPoint)_Socket.RemoteEndPoint).Address.ToString())}");
                    #endif
                    Byte[] bReceive = new Byte[1024];
                    _Socket.Receive(bReceive, bReceive.Length, 0);
                    string sBuffer = Encoding.ASCII.GetString(bReceive);
                    if (sBuffer.Substring(0, 3) != "GET")  
                    {
                        _Socket.Close();
                        return;
                    }
                    if (!Pages.ContainsKey(sBuffer.Substring(5).Split("HTTP/")[0].Replace(" ", "")))
                    {
                        string errorResult = _ErrorPage?.ToString() ?? "";
                        byte[] errorBytes = Encoding.ASCII.GetBytes($"HTTP/1.1 200 OK\r\nServer: cx1193719-b\r\nContent-Type: text/html\r\nAccept-Ranges: bytes\r\nAccess-Control-Allow-Origin: *\r\nContent-Length: {errorResult.Length} \r\n\r\n{errorResult}");
                        try  
                        {
                            _Socket.Send(errorBytes, errorBytes.Length, 0);
                        }
                        catch
                        {
                            throw new ImpartError("Error in sending packets to browser.");
                        }
                        _Socket.Close();
                    }
                    else
                    {
                        string result = Pages[sBuffer.Substring(5).Split("HTTP/")[0].Replace(" ", "")].ToString();
                        byte[] resultBytes = Encoding.ASCII.GetBytes($"HTTP/1.1 200 OK\r\nServer: cx1193719-b\r\nContent-Type: text/html\r\nAccept-Ranges: bytes\r\nAccess-Control-Allow-Origin: *\r\nContent-Length: {result.Length} \r\n\r\n{result}");
                        try
                        {
                            _Socket.Send(resultBytes, resultBytes.Length, 0);
                        }
                        catch
                        {
                            throw new ImpartError("Error in sending packets to browser.");
                        }
                        _Socket.Close();  
                    }
                    OnRequest?.Invoke(new WebsiteRequestArgs(sBuffer, IPAddress.Parse(((IPEndPoint)(_Socket.RemoteEndPoint ??)).Address.ToString())));
                }
            }
        }
    }
}