﻿using System.Net.Sockets;
using System.Text;

namespace Client
{
    class ClientApp : Menu
    {
        private static readonly Socket ClientSocket = new Socket
            (AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

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
            Console.WriteLine(@"Napisz ""stop"" by się poprawnie rozłączyć");

            while (true)
            {
                //EmailValidation();
                SendMessage();
                Console.Clear();
                ReceiveResponse();
                
            }
        }

        private static void Exit()//komenda na wyjscie 
        {
            SendString("stop");
            ClientSocket.Shutdown(SocketShutdown.Both);
            ClientSocket.Close();
            Environment.Exit(0);
        }

        private static void SendMessage()//wysyłanie
        {
             Console.Write("Wybierz Karte: ");
             string request = Console.ReadLine();
            SendString(request);

            if (request.ToLower() == "stop")
            {
                Exit();
            }
        }
        private static void EmailValidation()//Wybór użytkownika
        {
            Console.Write("Podaj e-mail: ");
            string request = Console.ReadLine();
            SendString(request);

            if (request.ToLower() == "kacper.pl")
            {
                MainM();
            }
            if (request.ToLower() == "sebastian.pl")
            {

            }
            if (request.ToLower() == "michal.pl")
            {

            }
        }

        private static void SendString(string text)//wysyłanie
        {
            byte[] buffer = Encoding.ASCII.GetBytes(text);
            ClientSocket.Send(buffer, 0, buffer.Length, SocketFlags.None);
        }

        private static void ReceiveResponse()//mail
        {
            var buffer = new byte[2048];
            int received = ClientSocket.Receive(buffer, SocketFlags.None);
            if (received == 0) return;
            var data = new byte[received];
            Array.Copy(buffer, data, received);
            string text = Encoding.ASCII.GetString(data);
            Console.WriteLine(text);
        }
    }
    }

