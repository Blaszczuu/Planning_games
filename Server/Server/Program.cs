using DataTransferObjects;
using Newtonsoft.Json;
using Server.Dto;
using Server.Services;
using System.ComponentModel;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace Server
{
    public class Program
    {
        private static readonly Socket serverSocket = new(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        private static readonly List<Client> connectedClients = new();
        private const int BUFFER_SIZE = 2048;
        private const int PORT = 100;
        public static readonly byte[] buffer = new byte[BUFFER_SIZE];


        private static IterationService ?iterationService;
        private static WorkItemsService ?workitemsx;
        private static TitleServices ?workTitles;
        private static List<TitleDto> workItems;

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
            foreach (Socket socket in connectedClients.Select(c => c.Socket))
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
                if (IP.AddressFamily == AddressFamily.InterNetwork)
                {
                    IPAddress = Convert.ToString(IP);
                }
            }
            socket.BeginReceive(buffer, 0, BUFFER_SIZE, SocketFlags.None, ReceiveCallback, socket);

            Console.WriteLine("Klient połączony, adres IP klienta: " + IPAddress);
            serverSocket.BeginAccept(AcceptCallback, null);
        }
        public static void ReceiveCallback(IAsyncResult AR)//otrzymywanie state(state= rodzaj opreacji do wykonania), usuwanie rozłączonego socketa, pokazywanie adresu IP,
        {
            Socket current = (Socket)AR.AsyncState;
            int received;
            string IPAddress = "";

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
            try
            {
                received = current!.EndReceive(AR);
            }
            catch (SocketException)
            {
                Console.WriteLine("Klient zamknął aplikacje, adres IP klienta: " + IPAddress);
                current!.Close();
                Client clientToDisconnect = connectedClients.Where(c => c.Socket == current).First();
                connectedClients.Remove(clientToDisconnect);
                return;
            }

            byte[] recBuf = new byte[received];
            Array.Copy(buffer, recBuf, received);
            string text = Encoding.ASCII.GetString(recBuf);
            Console.WriteLine("Otrzymany tekst: " + text);

            var resultCard = JsonSerializer.Deserialize<CardPacksRequest>(text);
            if (resultCard!.state == State.Cardsend)
            {
                CardCommunication(resultCard);
                ResultSend();

            }

            var resultI = JsonSerializer.Deserialize<EstimatedIRequest>(text);
            if (resultI!.state == State.Estiamtion)
            {
                ProblemEstimation(resultI);
            }

            var resultlogin = JsonSerializer.Deserialize<LoginRequest>(text);
            if (resultlogin!.state == State.Login)
            {
                EmailCheckCallBack(current, resultlogin);
            }
            var resultsprint = JsonSerializer.Deserialize<SprintReq>(text);
            if(resultsprint!.state == State.Sprint)
            {
                RestApi_Sprints(resultsprint);
            }

            current.BeginReceive(buffer, 0, BUFFER_SIZE, SocketFlags.None, ReceiveCallback, current);

        }
        private static void RestApi_Sprints(SprintReq sprintname)
        {
            List<TitleDto> workItems = new List<TitleDto>();
            HttpClient client = new HttpClient();

            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", "OnRvaHBzcXNycHlxcGV6b2xmeWNoYzdmNnpuaGZncnhzZmx2emVjM280aHdya2V0MnZleWE=");

            iterationService = new IterationService(client);
            workitemsx = new WorkItemsService(client);
            workTitles = new TitleServices(client);


            var sprints = iterationService.GetSprintIterations().Result;
            var sprintnamestr = sprintname.SprintName;
            List<WorkItemDto> witems = workitemsx.GetSprintwork(sprints.First(s => s.Name == sprintnamestr)).Result;

            foreach (var item in witems)
            {
                workItems.Add(workTitles.GetWorkItem(item).Result);
            }
            
            foreach (var workItem in workItems)
            {
                Console.WriteLine($"(#{workItem.Id}) {workItem.SystemTitle} ");
                var JsonSprintResponse = JsonSerializer.Serialize<SprintRes>(new SprintRes()
                {
                    SprintName = workItem.SystemTitle,
                    Id = workItem.Id
                });
                
                byte[] data = Encoding.ASCII.GetBytes(JsonSprintResponse);
                foreach (var socket in connectedClients.Select(c => c.Socket))
                {
                    connectedClients.Where(a => a.ClientRole == Role.ScrumMaster);
                    socket.Send(data);
                }
            }


        }

        private static void EmailCheckCallBack(Socket current, LoginRequest? resultlogin)// sprawdzanie loginu i wysyłanie odpowiedzi z rolą
        {

            if (resultlogin!.Email == "sebastian@abb.pl")
            {

                var jsonResponse = JsonSerializer.Serialize<LoginResponse>(new LoginResponse()
                {
                    email = resultlogin.Email,
                    role = Role.ScrumMaster,

                });

                connectedClients.Add(new Client(Role.ScrumMaster, current));

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
                connectedClients.Add(new Client(Role.ProductOwner, current));

                byte[] data = Encoding.ASCII.GetBytes(jsonResponse);
                current.Send(data);
            }
            else if (resultlogin.Email != null)
            {

                var response = new LoginResponse()
                {
                    email = resultlogin.Email,
                    role = Role.Developer,

                };
                var jsonResponse = JsonSerializer.Serialize<LoginResponse>(response);
                connectedClients.Add(new Client(Role.Developer, current));

                byte[] data = Encoding.ASCII.GetBytes(jsonResponse);
                current.Send(data);
            }
        }
        static int PlayersCount;
        private static void ProblemEstimation(EstimatedIRequest? resultI)//odbieranie id, wysyłanie do wszystkich, zliczanie do kogo wysłane
        {
            if (resultI!.ID != null)
            {
                var jsonResponse2 = JsonSerializer.Serialize<EstimatedIResponse>(new EstimatedIResponse()
                {
                    ID = resultI.ID,
                    Input = resultI.Input,
                    state = State.Estiamtion,
                });
                byte[] data = Encoding.ASCII.GetBytes(jsonResponse2);


                foreach (var socket in connectedClients.Select(c => c.Socket))
                {
                    socket.Send(data);
                    var UserResult = connectedClients.Where(a => a.ClientRole == Role.ScrumMaster);
                    int ur1 = UserResult.Take(Range.All).ToArray().Length;

                    var userResult2 = connectedClients.Where(a => a.ClientRole == Role.Developer);
                    int ur = userResult2.Take(Range.All).ToArray().Length;
                    PlayersCount = ur + ur1;
                }

            }
        }

        
        static List<int> votes = new List<int>();
        public static void CardCommunication(CardPacksRequest resultCard)//dodawanie kart do listy
        {
            votes.Add(resultCard.CardValue);
        }
        static int m;
        public static void ResultSend()//obliczanie wyniku
        {
            if (PlayersCount == votes.Count)
            {
                int result = Calculator.Calculate(votes);
               
                m = result;
                votes.Clear();
                SendingResult();
            }

        }

        private static void SendingResult()
        {
            var jsonResponse4 = JsonSerializer.Serialize<CardPacksResponse>(new CardPacksResponse()
            {
                CardResult = m,
                state = State.Cardresult
            });
            byte[] data = Encoding.ASCII.GetBytes(jsonResponse4);
            foreach (var socket in connectedClients.Select(c => c.Socket))
            {
                socket.Send(data);
            }
            votes.Clear();
            PlayersCount = 0;
        }
    }
}