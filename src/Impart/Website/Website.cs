using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Collections.Generic;
using Impart.Scripting;

namespace Impart
{
    public sealed class Website
    {
        private TcpListener listener;
        private int port;
        private WebPage errorPage;
        Dictionary<string, WebPage> webPages;
        Thread thread;
        public Action<WebsiteEventArgs> OnVisit;
        public Website(WebPage webPage, int port = 5050, string rootDirectory = "")
        {
            webPages = new Dictionary<string, WebPage>();
            webPages.Add(rootDirectory, webPage);
            this.port = port;
            errorPage = null;
        }
        public void Start()
        {
            try  
            {
                listener = new TcpListener(Dns.GetHostAddresses("localhost")[0], port);
                listener.Start();
                cancelToken = new CancellationTokenSource();
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
        public void AddPage(WebPage webPage, string directory)
        {
            if (webPages.ContainsKey(directory))
            {
                throw new ImpartError("Each webpage directory must be different!");
            }
            webPages.Add(directory, webPage);
        }
        public void Set404Page(WebPage webPage)
        {
            errorPage = webPage;
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
                    if (sBuffer.Substring(0, 3) != "GET")  
                    {
                        mySocket.Close();
                        return;
                    }
                    if (!webPages.ContainsKey(sBuffer.Substring(5).Split("HTTP/")[0].Replace(" ", "")))
                    {
                        string errorResult = errorPage?.GetCode() ?? "";
                        byte[] errorBytes = Encoding.ASCII.GetBytes($"HTTP/1.1 200 OK\r\nServer: cx1193719-b\r\nContent-Type: text/html\r\nAccept-Ranges: bytes\r\nContent-Length: {errorResult.Length} \r\n\r\n{errorResult}");
                        try  
                        {
                            mySocket.Send(errorBytes, errorBytes.Length, 0);
                        }
                        catch
                        {
                            throw new ImpartError("Error in sending packets to browser.");
                        }
                        mySocket.Close();
                    }
                    else
                    {
                        string result = webPages[sBuffer.Substring(5).Split("HTTP/")[0].Replace(" ", "")].GetCode();
                        Console.WriteLine(webPages[sBuffer.Substring(5).Split("HTTP/")[0].Replace(" ", "")].GetCode());
                        byte[] resultBytes = Encoding.ASCII.GetBytes($"HTTP/1.1 200 OK\r\nServer: cx1193719-b\r\nContent-Type: text/html\r\nAccept-Ranges: bytes\r\nContent-Length: {result.Length} \r\n\r\n{result}");
                        try
                        {
                            mySocket.Send(resultBytes, resultBytes.Length, 0);
                        }
                        catch
                        {
                            throw new ImpartError("Error in sending packets to browser.");
                        }
                        mySocket.Close();  
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
                        platformResult = Platform.Windows;
                        switch (browsers[13])
                        {
                            case "Windows":
                                platformResult = Platform.Windows;
                                break;
                            case "Linux":
                                platformResult = Platform.Linux;
                                break;
                            case "macOS":
                                platformResult = Platform.Mac;
                                break;
                            case "Android":
                                platformResult = Platform.Android;
                                break;
                            case "iOS":
                                platformResult = Platform.IOS;
                                break;
                            case "Chrome OS":
                                platformResult = Platform.Chrome;
                                break;
                            case "Unknown":
                                platformResult = Platform.Unknown;
                                break;
                        }
                        OnVisit?.Invoke(new WebsiteEventArgs(platformResult, browserList, mobileResult));
                    }
                }
            }
        }
    }
}