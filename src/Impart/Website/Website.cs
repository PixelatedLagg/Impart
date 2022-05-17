using System;
using System.Net;
using System.Text;
using System.Threading;
using Impart.Scripting;
using System.Net.Sockets;
using System.Collections.Generic;

namespace Impart
{
    /// <summary>The class responsible for hosting WebPages.</summary>
    public sealed class Website
    {
        private int _Port;

        /// <value>The value of the localhost port the Website is being hosted on.</value>
        public int Port
        {
            get
            {
                return _Port;
            }
        }

        /// <value>The method to be called when a client connects to the Website.</value>
        public Action<WebsiteEventArgs> OnVisit;

        private TcpListener _Listener;
        private WebPage _ErrorPage;
        private Dictionary<string, WebPage> _WebPages;
        private Thread _Thread;
        private Socket _Socket;

        /// <summary>Creates a Website instance.</summary>
        /// <param name="webPage">The default WebPage.</param>
        /// <param name="port">The local port to use. (Default is 5050)</param>
        /// <param name="rootDirectory">The subdomain for the default WebPage.</param>
        public Website(WebPage webPage, int port = 5050, string rootDirectory = "")
        {
            _WebPages = new Dictionary<string, WebPage>();
            _WebPages.Add(rootDirectory, webPage);
            _Port = port;
            _ErrorPage = null;
        }

        /// <summary>Start the Website.</summary>
        public void Start()
        {
            try  
            {
                _Listener = new TcpListener(Dns.GetHostAddresses("localhost")[0], _Port);
                _Listener.Start();
                _Thread = new Thread(new ThreadStart(StartListen));
                _Thread.Start();
                Console.BackgroundColor = ConsoleColor.Green;
                Console.ForegroundColor = ConsoleColor.Black;
                Console.WriteLine($" Website hosted on localhost:{_Port} ");
                Console.BackgroundColor = ConsoleColor.Black;
                Console.ForegroundColor = ConsoleColor.Gray;
            }
            catch
            {
                throw new ImpartError("Error in starting the Website.");
            }
        }

        /// <summary>Stop the Website.</summary>
        public void Stop() => _Listener.Stop();

        /// <summary>Add <paramref name="webPage"/> to the Website with <paramref name="directory"/> as the subdomain.</summary>
        /// <param name="webPage">The WebPage to add.</param>
        /// <param name="directory">The subdomain of the WebPage.</param>
        public void AddPage(WebPage webPage, string directory)
        {
            if (_WebPages.ContainsKey(directory))
            {
                throw new ImpartError("Each webpage subdomain must be different!");
            }
            _WebPages.Add(directory, webPage);
        }

        /// <summary>Remove the subdomain and the WebPage that goes along with it.</summary>
        /// <param name="directory">The subdomain of the Website.</param>
        public void RemovePage(string directory)
        {
            if (!_WebPages.ContainsKey(directory))
            {
                throw new ImpartError("The website does not contain this subdomain!");
            }
            _WebPages.Remove(directory);
        }

        /// <summary>Set the 404 page.</summary>
        /// <param name="webPage">The WebPage to set as the 404 page.</param>
        public void Set404Page(WebPage webPage)
        {
            _ErrorPage = webPage;
        }
        private void StartListen()
        {
            while (true)
            {
                _Socket = _Listener.AcceptSocket();
                if (_Socket.Connected) //socket.Connected
                {
                    Byte[] bReceive = new Byte[1024];
                    _Socket.Receive(bReceive, bReceive.Length, 0);
                    string sBuffer = Encoding.ASCII.GetString(bReceive);
                    if (sBuffer.Substring(0, 3) != "GET")  
                    {
                        _Socket.Close();
                        return;
                    }
                    if (!_WebPages.ContainsKey(sBuffer.Substring(5).Split("HTTP/")[0].Replace(" ", "")))
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
                        string result = _WebPages[sBuffer.Substring(5).Split("HTTP/")[0].Replace(" ", "")].ToString();
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
                    if (!String.IsNullOrWhiteSpace(OnVisit?.Method.ToString()))
                    {
                        string[] browsers = sBuffer.Split("sec-ch-ua:")[1].Split("\"");
                        List<(Browser, int)> browserList = new List<(Browser, int)>();
                        bool mobileResult;
                        Platform platformResult;
                        if (browsers[1].Contains("Brand"))
                        {
                            browsers[1] = "Not A Brand";
                        }
                        if (browsers[5].Contains("Brand"))
                        {
                            browsers[5] = "Not A Brand";
                        }
                        if (browsers[9].Contains("Brand"))
                        {
                            browsers[9] = "Not A Brand";
                        }
                        switch (browsers[1])
                        {
                            case "Not A Brand":
                                browserList.Add((Browser.NotBrand, Convert.ToInt32(browsers[3])));
                                break;
                            case "Opera":
                                browserList.Add((Browser.Opera, Convert.ToInt32(browsers[3])));
                                break;
                            case "Chromium":
                                browserList.Add((Browser.Chromium, Convert.ToInt32(browsers[3])));
                                break;
                            case "Google Chrome":
                                browserList.Add((Browser.Chrome, Convert.ToInt32(browsers[3])));
                                break;
                            case "Safari":
                                browserList.Add((Browser.Safari, Convert.ToInt32(browsers[3])));
                                break;
                            case "Maxthon":
                                browserList.Add((Browser.Maxthon, Convert.ToInt32(browsers[3])));
                                break;
                            case "Edge":
                                browserList.Add((Browser.Edge, Convert.ToInt32(browsers[3])));
                                break;
                            case "Firefox":
                                browserList.Add((Browser.Firefox, Convert.ToInt32(browsers[3])));
                                break;
                            case "Brave":
                                browserList.Add((Browser.Brave, Convert.ToInt32(browsers[3])));
                                break;
                            case "Vivaldi":
                                browserList.Add((Browser.Vivaldi, Convert.ToInt32(browsers[3])));
                                break;
                            case "Torch":
                                browserList.Add((Browser.Torch, Convert.ToInt32(browsers[3])));
                                break;
                            case "Netscape":
                                browserList.Add((Browser.Netscape, Convert.ToInt32(browsers[3])));
                                break;
                        }
                        switch (browsers[5])
                        {
                            case "Not A Brand":
                                browserList.Add((Browser.NotBrand, Convert.ToInt32(browsers[7])));
                                break;
                            case "Opera":
                                browserList.Add((Browser.Opera, Convert.ToInt32(browsers[7])));
                                break;
                            case "Chromium":
                                browserList.Add((Browser.Chromium, Convert.ToInt32(browsers[7])));
                                break;
                            case "Google Chrome":
                                browserList.Add((Browser.Chrome, Convert.ToInt32(browsers[7])));
                                break;
                            case "Safari":
                                browserList.Add((Browser.Safari, Convert.ToInt32(browsers[7])));
                                break;
                            case "Maxthon":
                                browserList.Add((Browser.Maxthon, Convert.ToInt32(browsers[7])));
                                break;
                            case "Edge":
                                browserList.Add((Browser.Edge, Convert.ToInt32(browsers[7])));
                                break;
                            case "Firefox":
                                browserList.Add((Browser.Firefox, Convert.ToInt32(browsers[7])));
                                break;
                            case "Brave":
                                browserList.Add((Browser.Brave, Convert.ToInt32(browsers[7])));
                                break;
                            case "Vivaldi":
                                browserList.Add((Browser.Vivaldi, Convert.ToInt32(browsers[7])));
                                break;
                            case "Torch":
                                browserList.Add((Browser.Torch, Convert.ToInt32(browsers[7])));
                                break;
                            case "Netscape":
                                browserList.Add((Browser.Netscape, Convert.ToInt32(browsers[7])));
                                break;
                        }
                        switch (browsers[9])
                        {
                            case "Not A Brand":
                                browserList.Add((Browser.NotBrand, Convert.ToInt32(browsers[11])));
                                break;
                            case "Opera":
                                browserList.Add((Browser.Opera, Convert.ToInt32(browsers[11])));
                                break;
                            case "Chromium":
                                browserList.Add((Browser.Chromium, Convert.ToInt32(browsers[11])));
                                break;
                            case "Google Chrome":
                                browserList.Add((Browser.Chrome, Convert.ToInt32(browsers[11])));
                                break;
                            case "Safari":
                                browserList.Add((Browser.Safari, Convert.ToInt32(browsers[11])));
                                break;
                            case "Maxthon":
                                browserList.Add((Browser.Maxthon, Convert.ToInt32(browsers[11])));
                                break;
                            case "Edge":
                                browserList.Add((Browser.Edge, Convert.ToInt32(browsers[11])));
                                break;
                            case "Firefox":
                                browserList.Add((Browser.Firefox, Convert.ToInt32(browsers[11])));
                                break;
                            case "Brave":
                                browserList.Add((Browser.Brave, Convert.ToInt32(browsers[11])));
                                break;
                            case "Vivaldi":
                                browserList.Add((Browser.Vivaldi, Convert.ToInt32(browsers[11])));
                                break;
                            case "Torch":
                                browserList.Add((Browser.Torch, Convert.ToInt32(browsers[11])));
                                break;
                            case "Netscape":
                                browserList.Add((Browser.Netscape, Convert.ToInt32(browsers[11])));
                                break;
                        }
                        if (browsers[12].Contains("?0"))
                        {
                            mobileResult = false;
                        }
                        else
                        {
                            mobileResult = true;
                        }
                        platformResult = browsers[13] switch 
                        {
                            "Windows" => Platform.Windows,
                            "Linux" => Platform.Linux,
                            "macOS" => Platform.Mac,
                            "Android" => Platform.Android,
                            "iOS" => Platform.IOS,
                            "Chrome OS" => Platform.Chrome,
                            _ => Platform.Unknown
                        };
                        OnVisit?.Invoke(new WebsiteEventArgs(platformResult, browserList, mobileResult));
                    }
                }
            }
        }
    }
}