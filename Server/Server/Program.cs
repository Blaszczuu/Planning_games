using DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;

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
            serverSocket.Listen();
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

            IPHostEntry? Host = default;
            string? Hostname = null;
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
        
            Console.WriteLine("Klient połączony, adres IP klienta: " + IPAddress);
            serverSocket.BeginAccept(AcceptCallback, null);
        }
        public static void ReceiveCallback(IAsyncResult AR)//otrzymywanie mail i wysyłka tematu
        {
            Socket current = (Socket)AR.AsyncState;
            int received;
            string IPAddress = "";

            IPHostEntry Host = default;
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
            try
            {
                received = current.EndReceive(AR);
            }
            catch (SocketException)
            {
                Console.WriteLine("Klient zamknął aplikacje, adres IP klienta: "+ IPAddress);
                current.Close();
                clientSockets.Remove(current);
                return;
            }

            byte[] recBuf = new byte[received];
            Array.Copy(buffer, recBuf, received);
            string text = Encoding.ASCII.GetString(recBuf);
            Console.WriteLine("Otrzymany tekst: " + text);
            
            var resultCard = JsonSerializer.Deserialize<CardPacksRequest>(text);

            if (resultCard.state == State.Cards)
            {
                CardCommunication(resultCard);
            }


            var resultI = JsonSerializer.Deserialize<EstimatedIRequest>(text);
            if (resultI.state ==State.Estiamtion)
            {
                ProblemEstimation(resultI);
            }

            var resultlogin = JsonSerializer.Deserialize<LoginRequest>(text);
            if (resultlogin.state==State.Login)
            {
                EmailCheckCallBack(current, resultlogin);
            }

            current.BeginReceive(buffer, 0, BUFFER_SIZE, SocketFlags.None, ReceiveCallback, current);
        }

        private static void EmailCheckCallBack(Socket current, LoginRequest? resultlogin)
        {
            if (resultlogin.Email == "sebastian@abb.pl")
            {

                var jsonResponse = JsonSerializer.Serialize<LoginResponse>(new LoginResponse()
                {
                    email = resultlogin.Email,
                    role = Role.ScrumMaster,

                });

                byte[] data = Encoding.ASCII.GetBytes(jsonResponse);
                current.Send(data);
            }
            else if (resultlogin.Email == "kacper@abb.pl")
            {
                var response = new LoginResponse()
                {
                    email = resultlogin.Email,
                    role = Role.ProductOwner,

                };
                var jsonResponse = JsonSerializer.Serialize<LoginResponse>(response);

                byte[] data = Encoding.ASCII.GetBytes(jsonResponse);
                current.Send(data);

            }
            else
            {
                var response = new LoginResponse()
                {
                    email = resultlogin.Email,
                    role = Role.Developer,

                };

                var jsonResponse = JsonSerializer.Serialize<LoginResponse>(response);

                byte[] data = Encoding.ASCII.GetBytes(jsonResponse);
                current.Send(data);
            }
        }

        private static void ProblemEstimation(EstimatedIRequest? resultI)
        {
            if (resultI.ID != null)
            {
                var jsonResponse2 = JsonSerializer.Serialize<EstimatedIResponse>(new EstimatedIResponse()
                {
                    ID = resultI.ID,
                    Input = resultI.Input,
                });
                byte[] data = Encoding.ASCII.GetBytes(jsonResponse2);

                foreach (var socket in clientSockets)
                {
                    socket.Send(data);
                }
            }
        }

        private static void CardCommunication(CardPacksRequest? resultCard)
        {
            var jsonresponse3 = JsonSerializer.Serialize<CardPacksResponse>(new CardPacksResponse()
            {
                Cards = resultCard.Cards,

            });
            byte[] data = Encoding.ASCII.GetBytes(jsonresponse3);

            foreach (var socket1 in clientSockets)
            {
                socket1.Send(data);
            }
        }
    }
}