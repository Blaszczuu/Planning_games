using DataTransferObjects;
using System.Net.Sockets;

namespace Server
{
    public class Client
    {
        public Client(Role clientRole, Socket socket)
        {
            ClientRole = clientRole;
            Socket = socket;
        }

        public Role ClientRole { get; }
        public Socket Socket { get; }

    }
}