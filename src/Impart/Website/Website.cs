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
        CancellationTokenSource cancelToken;
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
        public void Stop()
        {
            //cancelToken.Cancel();
            Environment.Exit(0); //exit all threads for now
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
                        Console.WriteLine($"{browsers[3]} {browsers[7]} {browsers[11]}");
                        List<(Browser, int)> browserList = new List<(Browser, int)>();
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
                            case string a when a.Contains("Brand"):
                                browserList.Add((Browser.NotBrand, Convert.ToInt32(browsers[3])));
                                break;
                            case "Opera":
                                browserList.Add((Browser.Opera, Convert.ToInt32(browsers[3])));
                                break;
                            case "Chromium":
                                browserList.Add((Browser.Chromium, Convert.ToInt32(browsers[3])));
                                break;
                        }
                    }
                    Events.ParseRequest(sBuffer);
                }
            }
        }
    }
}