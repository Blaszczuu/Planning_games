using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Server
{
    public class Program
    {
        
        private static readonly Socket serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        private static readonly List<Socket> clientSockets = new List<Socket>();
        private const int BUFFER_SIZE = 2048;
        private const int PORT = 100;
        public static readonly byte[] buffer = new byte[BUFFER_SIZE];

        public static void Main()
        {
            Console.Title = "Server: " + Dns.GetHostEntry(Dns.GetHostName()).AddressList.First(a => a.AddressFamily == AddressFamily.InterNetwork);
            SetupServer();
            Console.ReadLine(); 
            CloseAllSockets();
        }

        public static void SetupServer()//inicjacja serwera
        {
            Console.WriteLine("Konfiguracja servera...");
            serverSocket.Bind(new IPEndPoint(IPAddress.Any, PORT));
            serverSocket.Listen(0);
            serverSocket.BeginAccept(AcceptCallback, null);
            Console.WriteLine("Server skonfigurowany pomyślnie");
        }

        public static void CloseAllSockets()//wylaczenie socketow
        {
            foreach (Socket socket in clientSockets)
            {
                socket.Shutdown(SocketShutdown.Both);
                socket.Close();
            }

            serverSocket.Close();
        }
        public static void GetIP()
        {
            {
                string IPAddress = "";
                IPHostEntry Host = default(IPHostEntry);
                string Hostname = null;
                Hostname = System.Environment.MachineName;
                Host = Dns.GetHostEntry(Hostname);
                foreach (IPAddress IP in Host.AddressList)
                {
                    if (IP.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                    {
                        IPAddress = Convert.ToString(IP);
                    }
                }
            }
        }
        public static void AcceptCallback(IAsyncResult AR)//akceptacja polaczenia od klienta
        {
            Socket socket;
            string IPAddress = "";

            try
            {
                socket = serverSocket.EndAccept(AR);
            }
            catch (ObjectDisposedException) 
            {
                return;
            }
            
                IPHostEntry Host = default(IPHostEntry);
                string Hostname = null;
                Hostname = System.Environment.MachineName;
                Host = Dns.GetHostEntry(Hostname);
                foreach (IPAddress IP in Host.AddressList)
                {
                    if (IP.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                    {
                        IPAddress = Convert.ToString(IP);
                    }
                }
            

            clientSockets.Add(socket);
            socket.BeginReceive(buffer, 0, BUFFER_SIZE, SocketFlags.None, ReceiveCallback, socket);
            Console.WriteLine("Klient połączony, adres IP klienta: "+ IPAddress) ;
            serverSocket.BeginAccept(AcceptCallback, null);
        }

        public static void ReceiveCallback(IAsyncResult AR)//otrzymywanie wiadomosci
        {
            Socket current = (Socket)AR.AsyncState;
            int received;
            string IPAddress = "";

            try
            {
                received = current.EndReceive(AR);
            }
            catch (SocketException)
            {
                Console.WriteLine("Klient wymusił zamknięcie aplikacji, bez poprawnego wyjścia");
                current.Close();
                clientSockets.Remove(current);
                return;
            }

            byte[] recBuf = new byte[received];
            Array.Copy(buffer, recBuf, received);
            string text = Encoding.ASCII.GetString(recBuf);
            Console.WriteLine("Otrzymany tekst: " + text);

            IPHostEntry Host = default(IPHostEntry);
            string Hostname = null;
            Hostname = System.Environment.MachineName;
            Host = Dns.GetHostEntry(Hostname);
            foreach (IPAddress IP in Host.AddressList)
            {
                if (IP.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                {
                    IPAddress = Convert.ToString(IP);
                }
            }


            if (text.ToLower() == "stop") // klient wyszedł poprawnie
            {
                current.Shutdown(SocketShutdown.Both);
                current.Close();
                clientSockets.Remove(current);
                Console.WriteLine("Klient rozłączył się, Adres Ip Klienta: "+IPAddress);
                return;
            }
            if (text.ToLower() == "kacper.pl")
            {
                byte[] data = Encoding.ASCII.GetBytes("Witamy ScrumMastera");
                current.Send(data);
            }
            if (text.ToLower() == "sebastian.pl")
            {
                byte[] data = Encoding.ASCII.GetBytes("Witamy ProductOwnera");
                current.Send(data);
            }
            if (text.ToLower() == "michal.pl")
            {
                byte[] data = Encoding.ASCII.GetBytes("Witamy Developera");
                current.Send(data);
            }
            current.BeginReceive(buffer, 0, BUFFER_SIZE, SocketFlags.None, ReceiveCallback, current);
        }

    }
}