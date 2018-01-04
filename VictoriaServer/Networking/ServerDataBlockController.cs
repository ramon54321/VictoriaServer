using RamonBrand.Networking;
using System;
using System.Collections.Generic;
using System.Text;
using Ether.Network;

namespace VictoriaServer.Networking
{
    class ServerDataBlockController : DataBlockController
    {
        protected override void Action(DataBlock dataBlock, NetConnection netConnection)
        {
            throw new NotImplementedException();
        }

        protected override void ActionRequest(DataBlock dataBlock, NetConnection netConnection)
        {
            throw new NotImplementedException();
        }

        protected override void Admin(DataBlock dataBlock, NetConnection netConnection)
        {
            try
            {
                Client client = (Client)netConnection;

                // -- Admin messages - Type 0
                string[] data = dataBlock.body.Split('#');

                // -- Console Log
                if (data[0] == "cl")
                {
                    Console.WriteLine(client.GetShortId() + " LOGS: " + data[1]);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error Parsing: " + dataBlock.body);
            }
        }
    }
}
