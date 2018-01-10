using System;
using Ether.Network;
using SharpLogger;

namespace VictoriaServer.Networking
{
    class NetworkSocket : NetServer<Client>
    {
        private readonly NetworkManager _networkManager;

        public NetworkSocket()
        {
            this._networkManager = NetworkManager.GetInstance();
            this.Configuration.Backlog = 100;
            this.Configuration.Port = 8888;
            this.Configuration.MaximumNumberOfConnections = 16;
            this.Configuration.Host = "127.0.0.1";
            this.Configuration.BufferSize = 4096;
        }

        protected override void Initialize()
        {
            Logger.Log(LogLevel.L2_Info, "NetworkSocket is listening.", "Network");
        }

        protected override void OnClientConnected(Client client)
        {
            _networkManager.ClientConnected(client);
        }

        protected override void OnClientDisconnected(Client client)
        {
            _networkManager.ClientDisconnected(client);
        }

        protected override void OnError(Exception exception)
        {
            throw exception;
        }
    }
}
