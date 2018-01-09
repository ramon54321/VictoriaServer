using System;
using System.Collections.Generic;
using System.Text;
using Ether.Network;
using VictoriaShared.Networking;

namespace VictoriaServer.Networking
{
    class NetworkManager
    {
        private static NetworkManager instance = null;
        public static NetworkManager GetInstance()
        {
            if (instance == null)
                instance = new NetworkManager();

            return instance;
        }
        private NetworkManager()
        {
            dataBlockController = new ServerDataBlockController();
            clientList = new Dictionary<Guid, Client>();
        }

        private DataBlockController dataBlockController;
        private Dictionary<Guid, Client> clientList;

        public void ProcessDataBlock(DataBlock dataBlock, NetConnection netConnection)
        {
            dataBlockController.ProcessDataBlock(dataBlock, netConnection);
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
    }
}
