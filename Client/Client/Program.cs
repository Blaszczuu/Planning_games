using System;
using System.Net.Sockets;
using System.Net;
using System.Text;

namespace MultiClient
{
    class Program
    {
        private static readonly Socket ClientSocket = new Socket
            (AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

        private const int PORT = 100;

        static void Main()
        {

            Console.Title = "Client";
            ConnectToServer();
            RequestLoop();
            Exit();
        }

        private static void ConnectToServer()//łączenie z serwerem
        {
            int attempts = 0;

            while (!ClientSocket.Connected)
            {
                try
                {
                    attempts++;
                    Console.WriteLine("Próba połączenia " + attempts);
                    // połączenie na kilku komputerach wymaga zmiany adresu, na adres komputera z którego zostanie włączony serwer "10.3.39.25"
                    ClientSocket.Connect(IPAddress.Loopback, PORT);
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
                SendText();
                //check();
                //ReceiveResponse();
            }
        }

        private static void Exit()//komenda na wyjscie 
        {
            SendString("stop");
            ClientSocket.Shutdown(SocketShutdown.Both);
            ClientSocket.Close();
            Environment.Exit(0);
        }

        private static void SendText()//wysyłanie
        {
            Console.Write("Wybierz kartę: ");
            string request = Console.ReadLine();
            SendString(request);

            if (request.ToLower() == "stop")
            {
                Exit();
            }

        

        }

        private static void SendString(string text)//wysyłanie
        {
            byte[] buffer = Encoding.ASCII.GetBytes(text);
            ClientSocket.Send(buffer, 0, buffer.Length, SocketFlags.None);
        }

        private static void ReceiveResponse()//głos na rundę
        {
            var buffer = new byte[2048];
            int received = ClientSocket.Receive(buffer, SocketFlags.None);
            if (received == 0) return;
            var data = new byte[received];
            Array.Copy(buffer, data, received);
            string text = Encoding.ASCII.GetString(data);
            Console.WriteLine(text);
        }
        private static void check()//sprawdzanie czy liczba
        {
            double number;
            while (!double.TryParse(Console.ReadLine(), out number))
            {
                Console.Write("To nie jest liczba, proszę nie psuć programu :(\n");
                Console.Write("Napisz wiadomość: ");

            }
        }
        

    }
    }

