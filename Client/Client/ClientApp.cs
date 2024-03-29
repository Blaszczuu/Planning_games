using System.Net.Sockets;
using System.Text;
using System.ComponentModel.DataAnnotations;
using DataTransferObjects;
using System.Text.Json;
using System.Globalization;

namespace Client
{
    class ClientApp
    {
        private static readonly Socket ClientSocket = new(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

        private const int PORT = 100;
        private static List<int> sprintid = new List<int>();
        private static List<string> sprinttitle = new List<string>();

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
            String? host = Console.ReadLine();

            while (!ClientSocket.Connected)
            {
                try
                {
                    attempts++;
                    Console.WriteLine("Próba połączenia " + attempts);

                    ClientSocket.Connect(host!, PORT);
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
            while (!Receivemail())
            {
            }
        }
        public static void SendCard()
        {
            Console.Write("Wybierz Kartę: ");
            int card;
            while (!int.TryParse(Console.ReadLine(), out card))
            {
                Console.Write("Wprowadzony tekst nie jest wartością, wprowadź wartość karty: ");
            }
            SendCardReq(card);
        }

        private static void SendCardReq(int value)
        {
            CardPacksRequest CardRequest = new()
            {
                CardValue = value,
                state = State.Cardsend
            };

            string json = JsonSerializer.Serialize(CardRequest);
            SendInt(json);

        }
        public static void GetSprintName()
        {
            Console.WriteLine("Podaj nazwe sprintu");
            string? sprint = Console.ReadLine();
            if (sprint == null)
            {
                throw new Exception();
            }
            SendSprintReq(sprint);

        }
        private static void SendSprintReq(string sprint)
        {
            SprintReq SprintReq = new()
            {
                SprintName = sprint,
                state = State.Sprint,
            };
            string jsonSprint = JsonSerializer.Serialize(SprintReq);
            SendString(jsonSprint);
        }
        public static void SendI()
        {
            
            int scount = sprintid.Count;
            for (int i = 0; i < scount; i++)
            {
                int? ID = sprintid[i];
                string IDstring = ID.ToString();
                string? Txt = sprinttitle[i];
                
                SendIRequest(IDstring!, Txt!);
                while (!ClientApp.ReceiveID())
                {
                }
                ClientApp.SendCard();
                while (!ClientApp.ReceiveResult())
                {
                }
                Console.ReadLine();
                Thread.Sleep(100); 
            }
            sprintid.Clear();
        }

        private static void SendIRequest(string IDProblem, string ProblemTxt)
        {
            EstimatedIRequest EstimatedIRequest = new()
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
            string? email = Console.ReadLine();
            SendLoginRequest(email!);
        }

        private static void SendLoginRequest(string text)
        {
            LoginRequest loginRequest = new()
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

        public static bool Receivemail()//mail
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

            var result = JsonSerializer.Deserialize<LoginResponse>(text);
            if (result!.role == Role.ScrumMaster)
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
            if (resultI!.ID != null)
            {
                Console.Clear();
                Console.WriteLine("ID Estymowanego tematu: " + resultI.ID + "\nEstymowany temat: " + resultI.Input);

                return true;
            }
            
            return false;
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

            var receiveresult = JsonSerializer.Deserialize<CardPacksResponse>(text);
            if (receiveresult!.CardResult > -1)
            {
                Console.WriteLine("Wynik głosowania: " + receiveresult.CardResult);
            }
            return true;
        }
        
        public static bool ReceiveSprintDetails()
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

            var receivedSprints = JsonSerializer.Deserialize<List<SprintRes>>(text);
            

            foreach (var item in receivedSprints)
            {
                sprintid.Add(item.Id);
                sprinttitle.Add(item.SystemTitle);
                if (item!.SystemTitle != null)
                {
                    Console.WriteLine(item.Id + " " + item.SystemTitle);
                }
            }
            return true;
        }
    }
}