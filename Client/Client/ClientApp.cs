﻿using System.Net.Sockets;
using System.Text;
using System.ComponentModel.DataAnnotations;
using DataTransferObjects;
using System.Text.Json;

namespace Client
{
    class ClientApp
    {
        private static readonly Socket ClientSocket = new (AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

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
            string card = Console.ReadLine();
            SendCardReq(card); 
        }
        private static void SendCardReq(string tekst)
        {
            CardPacksRequest CardRequest = new CardPacksRequest()
            {
                Cards =  tekst.Split('\u002C'),
            };

            string json = JsonSerializer.Serialize(CardRequest);
            SendString(json);

        }
        public static void SendI()
        {
            Console.WriteLine("Podaj ID Rozgrywki: ");
            string ID = Console.ReadLine();
            Console.WriteLine("Podaj Temat Rozgrywki: ");
            string Txt = Console.ReadLine();
            SendIRequest(ID,Txt);
        }
        public static void SendIRequest(string IDProblem, string ProblemTxt)
        {
            EstimatedIRequest EstimatedIRequest = new EstimatedIRequest()
            {
                ID = IDProblem,
                Input = ProblemTxt
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
                Email = text
            };

            string json = JsonSerializer.Serialize(loginRequest);
            SendString(json);
        }

        public static void SendString(string text)//wysyłanie
        {
            byte[] buffer = Encoding.ASCII.GetBytes(text);
            ClientSocket.Send(buffer, 0, buffer.Length, SocketFlags.None);
        }

        private static bool ReceiveResponse()//mail
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
            }
            else
            {
                Console.Clear();
                Console.Write(result.email + " zalogowany jako " + result.role);
                DevMenu.DevM();
            }
            return false;
        }
        
    }
    }

