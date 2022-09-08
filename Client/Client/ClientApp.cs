
﻿using System.Net.Sockets;
using System.Text;
using System.ComponentModel.DataAnnotations;
using DataTransferObjects;
using System.Text.Json;

namespace Client
{
    class ClientApp
    {
        private static readonly Socket ClientSocket = new(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

        private const int PORT = 100;

        static void Main()
        {
            Console.Title = "Client";
            ConnectToServer();
            RequestLoop();
        }

        private static void ConnectToServer()//łączenie z serwerem
        {
            int attempts = 0;
            Console.WriteLine("Podaj adres IP do którego chcesz się połączyć");
            String host = Console.ReadLine();

            while (!ClientSocket.Connected)
            {
                try
                {
                    attempts++;
                    Console.WriteLine("Próba połączenia " + attempts);

                    ClientSocket.Connect(host, PORT);
                }
                catch (SocketException)
                {
                    Console.Clear();
                }
            }

            Console.Clear();
            Console.WriteLine("Połączony");
        }

        private static void RequestLoop()//obsługa wysyłki
        {
            Console.WriteLine(@"Podaj swój e-mail by zalogować się do konta");
            SendMail();
            while (!ReceiveResponse())
            {
            }

        }
        public static void SendCard()
        {
            Console.Write("Wybierz Kartę: ");
            int card = int.Parse(Console.ReadLine());
            SendCardReq(card);
        }
        private static void SendCardReq(int value)
        {
            CardPacksRequest CardRequest = new()
            {
                CardValue = value,
                state = State.Cards
            };

            string json = JsonSerializer.Serialize(CardRequest);
            SendInt(json);

        }
        public static void SendI()
        {
            Console.WriteLine("Podaj ID Rozgrywki: ");
            string ID = Console.ReadLine();
            Console.WriteLine("Podaj Temat Rozgrywki: ");
            string Txt = Console.ReadLine();
            SendIRequest(ID, Txt);
        }
        public static void SendIRequest(string IDProblem, string ProblemTxt)
        {
            EstimatedIRequest EstimatedIRequest = new EstimatedIRequest()
            {
                ID = IDProblem,
                Input = ProblemTxt,
                state = State.Estiamtion
            };

            string json = JsonSerializer.Serialize(EstimatedIRequest);
            SendString(json);
        }
        public static void SendMail()//wysyłanie
        {
            Console.Write("Wyślij do servera: ");
            string email = Console.ReadLine();
            SendLoginRequest(email);
        }

        private static void SendLoginRequest(string text)
        {
            LoginRequest loginRequest = new LoginRequest()
            {
                Email = text,
                state = State.Login
            };

            string json = JsonSerializer.Serialize(loginRequest);
            SendString(json);
        }

        public static void SendInt(string text)//wysyłanie
        {
            byte[] buffer = Encoding.ASCII.GetBytes(text);
            ClientSocket.Send(buffer, 0, buffer.Length, SocketFlags.None);
        }
        public static void SendString(string text)//wysyłanie
        {
            byte[] buffer = Encoding.ASCII.GetBytes(text);
            ClientSocket.Send(buffer, 0, buffer.Length, SocketFlags.None);
        }
        public static bool ReceiveCard()
        {
            var buffer = new byte[2048];

            int received = 0;

            if (ClientSocket.Available > 0)
            {
                received = ClientSocket.Receive(buffer, SocketFlags.None);
            }
            if (received == 0) return false;
            var data = new byte[received];
            Array.Copy(buffer, data, received);
            string text = Encoding.ASCII.GetString(data);


            var resultCard = JsonSerializer.Deserialize<CardPacksResponse>(text);
            if (resultCard != null)
            {
                Console.WriteLine(resultCard);
            }
            return false;   

        }

        public static bool ReceiveResponse()//mail
        {
            var buffer = new byte[2048];
            int received = ClientSocket.Receive(buffer, SocketFlags.None);
            if (received == 0) return false;
            var data = new byte[received];
            Array.Copy(buffer, data, received);
            string text = Encoding.ASCII.GetString(data);



            var result = JsonSerializer.Deserialize<LoginResponse>(text);

            if (result.role == Role.ScrumMaster)
            {
                Console.Clear();
                Console.Write(result.email + " zalogowany jako " + result.role);
                Menu.MainM();
            }
            else if (result.role == Role.ProductOwner)
            {
                Console.Clear();
                Console.Write(result.email + " zalogowany jako " + result.role);
                ProductOwner.PoM();
            }
            else if (result.role == Role.Developer)
            {
                Console.Clear();
                Console.Write(result.email + " zalogowany jako " + result.role);
                DevMenu.DevM();
            }

            return false;
        }
        public static bool ReceiveID()
        {
            var buffer = new byte[2048];

            int received = 0;

            if (ClientSocket.Available > 0)
            {
                received = ClientSocket.Receive(buffer, SocketFlags.None);
            }

            if (received == 0) return false;
            var data = new byte[received];
            Array.Copy(buffer, data, received);
            string text = Encoding.ASCII.GetString(data);

            var resultI = JsonSerializer.Deserialize<EstimatedIResponse>(text);
            if (resultI.ID != null)
            {
                Console.Clear();
                Console.WriteLine("ID Estymowanego tematu: "+resultI.ID +"\nEstymowany temat: "+ resultI.Input);
            }
            return true;
        }
        public static bool ReceiveResult()
        {
            var buffer = new byte[2048];

            int received = 0;

            if (ClientSocket.Available > 0)
            {
                received = ClientSocket.Receive(buffer, SocketFlags.None);
            }
            if (received == 0) return false;
            var data = new byte[received];
            Array.Copy(buffer, data, received);
            string text = Encoding.ASCII.GetString(data);

            var receiveresult = JsonSerializer.Deserialize<VoteResult>(text);
            if (receiveresult.Result != null)
            {
                Console.WriteLine("Wynik głosowania: " + receiveresult.Result);
            }
            return true;
        }
    }
}