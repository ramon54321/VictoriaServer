using System;
using System.Collections.Generic;
using System.Text;
using Ether.Network;
using Ether.Network.Packets;
using VictoriaShared.Networking;

namespace VictoriaServer.Networking
{
    class Client : NetConnection
    {
        public override void HandleMessage(NetPacketBase packet)
        {
            // -- Create datablock from string
            DataBlock dataBlock = new DataBlock(Encoding.ASCII.GetBytes(packet.Read<string>()));

            // -- Pass datablock to datablock controller
            NetworkManager.GetInstance().ProcessDataBlock(dataBlock, this);
        }

        public void SendDataBlock(DataBlock dataBlock)
        {
            using (NetPacket packet = new NetPacket())
            {
                // -- Create string from datablock bytes
                packet.Write(Encoding.ASCII.GetString(dataBlock.GetBytes()));
                this.Send(packet);
            }
        }

        public string GetShortId()
        {
            return this.Id.ToString().Substring(0, 4);
        }
    }
}
