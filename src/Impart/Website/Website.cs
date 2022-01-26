using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Collections.Generic;

namespace Impart
{
    public sealed class Website
    {
        private TcpListener listener;
        private int port;
        private WebPage errorPage;
        private string rootDirectory;
        Dictionary<string, WebPage> webPages;
        public Website(WebPage webPage, int port = 5050, string rootDirectory = "")
        {
            webPages = new Dictionary<string, WebPage>();
            webPages.Add("/", webPage);
            this.port = port;
            errorPage = null;
            this.rootDirectory = rootDirectory;
        }
        public void Start()
        {
            try  
            {
                listener = new TcpListener(Dns.GetHostAddresses("localhost")[0], port);
                listener.Start();
                Thread thread = new Thread(new ThreadStart(StartListen));
                thread.Start();
                Console.WriteLine($"Website hosted on localhost:{port}");
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
                    mySocket.Receive(bReceive,bReceive.Length,0);
                    string sBuffer = Encoding.ASCII.GetString(bReceive);
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
                }
            }  
        }
    }
}