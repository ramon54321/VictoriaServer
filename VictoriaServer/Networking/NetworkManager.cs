using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Ether.Network;
using VictoriaShared.Networking;

namespace VictoriaServer.Networking
{
    class NetworkManager
    {
        private static NetworkManager _instance = null;
        public static NetworkManager GetInstance()
        {
            if (_instance == null)
                _instance = new NetworkManager();

            return _instance;
        }
        private NetworkManager()
        {
            _instance = this;

            clientList = new Dictionary<Guid, Client>();

            // -- Start Listening
            _networkSocket = new NetworkSocket();
            _networkThread = new Thread(delegate ()
            {
                _networkSocket.Start();
            });
            _networkThread.Name = "Network Thread";
            _networkThread.Start();
        }

        private Dictionary<Guid, Client> clientList;
        private Thread _networkThread;
        private NetworkSocket _networkSocket;

        public void CloseSocket()
        {
            _networkSocket.Stop();
        }

        public void SendToClient(Guid guid, DataBlock dataBlock)
        {
            clientList[guid].SendDataBlock(dataBlock);
        }

        public void SendToAllClients(DataBlock dataBlock)
        {
            foreach(Client client in clientList.Values)
            {
                client.SendDataBlock(dataBlock);
            }
        }

        public void SendToAllButOneClient(Guid guid, DataBlock dataBlock)
        {
            foreach (Client client in clientList.Values)
            {
                if(client.Id != guid)
                    client.SendDataBlock(dataBlock);
            }
        }

        public void ClientConnected(Client client)
        { 
            Console.WriteLine("Client Connected");
            clientList.Add(client.Id, client);
        }

        public void ClientDisconnected(Client client)
        {
            Console.WriteLine("Client Disconnected");
            clientList.Remove(client.Id);
        }

        /**
         * This method is called when a message is received from a client, and has been converted to a datablock object.
         *
         * Datablocks are split into 4 groups.
         *
         * Check which group the datablock belongs to.
         * Send Requests to the RequestManager to deal with.
         * Send Admin datablocks to the AdminManager to deal with.
         */
        public void ProcessDataBlock(DataBlock dataBlock, NetConnection netConnection)
        {
            // -- Admin
            if (dataBlock.function < DataBlockFunction.ADMIN)
            {
                throw new NotImplementedException();
            }
            // -- Request
            else if (dataBlock.function < DataBlockFunction.REQUEST)
            {
                throw new NotImplementedException();
            }
            // -- Action
            else if (dataBlock.function < DataBlockFunction.ACTION)
            {
                throw new NotImplementedException();
            }
            // -- Event
            else
            {
                throw new NotImplementedException();
            }
        }
    }
}
