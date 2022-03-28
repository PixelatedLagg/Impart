using System;
using System.Net;
using System.Text;
using System.Threading;
using Impart.Scripting;
using System.Net.Sockets;
using System.Collections.Generic;

namespace Impart
{
    public sealed class Website
    {
        public Action<WebsiteEventArgs> OnVisit;
        private TcpListener listener;
        private int port;
        private WebPage errorPage;
        private Dictionary<string, WebPage> webPages;
        private Thread thread;
        private Socket socket;

        /// <summary>Creates a Website instance with <paramref name="webPage"/>.</summary>
        /// <returns>A Website instance.</returns>
        /// <param name="webPage">The default WebPage.</param>
        /// <param name="port">The local port to host it on.</param>
        /// <param name="rootDirectory">The subdomain for the default WebPage.</param>
        public Website(WebPage webPage, int port = 5050, string rootDirectory = "")
        {
            webPages = new Dictionary<string, WebPage>();
            webPages.Add(rootDirectory, webPage);
            this.port = port;
            errorPage = null;
        }

        /// <summary>Start the Website.</summary>
        public void Start()
        {
            try  
            {
                listener = new TcpListener(Dns.GetHostAddresses("localhost")[0], port);
                listener.Start();
                thread = new Thread(new ThreadStart(StartListen));
                thread.Start();
                Console.BackgroundColor = ConsoleColor.Green;
                Console.ForegroundColor = ConsoleColor.Black;
                Console.WriteLine($" Website hosted on localhost:{port} ");
                Console.BackgroundColor = ConsoleColor.Black;
                Console.ForegroundColor = ConsoleColor.Gray;
            }
            catch
            {
                throw new ImpartError("Error in starting the website.");
            }
        }

        /// <summary>Stop the Website.</summary>
        public void Stop()
        {
            Environment.Exit(0);
        }

        /// <summary>Add <paramref name="webPage"/> to the Website with <paramref name="directory"/> as the subdomain.</summary>
        /// <param name="webPage">The WebPage to add.</param>
        /// <param name="directory">The subdomain of the WebPage.</param>
        public void AddPage(WebPage webPage, string directory)
        {
            if (webPages.ContainsKey(directory))
            {
                throw new ImpartError("Each webpage subdomain must be different!");
            }
            webPages.Add(directory, webPage);
        }

        /// <summary>Remove the subdomain <param name="directory">, and the WebPage that goes along with it.</summary>
        /// <param name="directory">The subdomain of the Website.</param>
        public void RemovePage(string directory)
        {
            if (!webPages.ContainsKey(directory))
            {
                throw new ImpartError("The website does not contain this subdomain!");
            }
            webPages.Remove(directory);
        }

        /// <summary>Set <param name="webPage"> as the 404 page.</summary>
        /// <param name="webPage">The WebPage to set as the 404 page.</param>
        public void Set404Page(WebPage webPage)
        {
            errorPage = webPage;
        }
        private void StartListen()
        {
            while (true)
            {
                socket = listener.AcceptSocket();
                if (socket.Connected) //socket.Connected
                {
                    Byte[] bReceive = new Byte[1024];
                    socket.Receive(bReceive, bReceive.Length, 0);
                    string sBuffer = Encoding.ASCII.GetString(bReceive);
                    if (sBuffer.Substring(0, 3) != "GET")  
                    {
                        socket.Close();
                        return;
                    }
                    if (!webPages.ContainsKey(sBuffer.Substring(5).Split("HTTP/")[0].Replace(" ", "")))
                    {
                        string errorResult = errorPage?.GetCode() ?? "";
                        byte[] errorBytes = Encoding.ASCII.GetBytes($"HTTP/1.1 200 OK\r\nServer: cx1193719-b\r\nContent-Type: text/html\r\nAccept-Ranges: bytes\r\nAccess-Control-Allow-Origin: *\r\nContent-Length: {errorResult.Length} \r\n\r\n{errorResult}");
                        try  
                        {
                            socket.Send(errorBytes, errorBytes.Length, 0);
                        }
                        catch
                        {
                            throw new ImpartError("Error in sending packets to browser.");
                        }
                        socket.Close();
                    }
                    else
                    {
                        string result = webPages[sBuffer.Substring(5).Split("HTTP/")[0].Replace(" ", "")].GetCode();
                        byte[] resultBytes = Encoding.ASCII.GetBytes($"HTTP/1.1 200 OK\r\nServer: cx1193719-b\r\nContent-Type: text/html\r\nAccept-Ranges: bytes\r\nAccess-Control-Allow-Origin: *\r\nContent-Length: {result.Length} \r\n\r\n{result}");
                        try
                        {
                            socket.Send(resultBytes, resultBytes.Length, 0);
                        }
                        catch
                        {
                            throw new ImpartError("Error in sending packets to browser.");
                        }
                        socket.Close();  
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