using System;
using System.Collections.Generic;
using System.Text;
using Ether.Network;
using Ether.Network.Packets;

namespace VictoriaServer.Networking
{
    class Server : NetServer<Client>
    {
        public Server()
        {
            this.Configuration.Backlog = 100;
            this.Configuration.Port = 8888;
            this.Configuration.MaximumNumberOfConnections = 16;
            this.Configuration.Host = "127.0.0.1";
            this.Configuration.BufferSize = 4096;
        }

        protected override void Initialize()
        {
            Console.WriteLine("Server is listening.");
        }

        protected override void OnClientConnected(Client client)
        {
            NetworkManager.GetInstance().ClientConnected(client);
        }

        protected override void OnClientDisconnected(Client client)
        {
            NetworkManager.GetInstance().ClientDisconnected(client);
        }

        protected override void OnError(Exception exception)
        {
            throw exception;
        }
    }
}
