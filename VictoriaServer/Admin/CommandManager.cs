using RamonBrand.Networking;
using System;
using System.Collections.Generic;
using System.Text;
using VictoriaServer.Networking;

namespace VictoriaServer.Admin
{
    class CommandManager
    {
        public CommandManager()
        {
            this.Listen();
        }

        private void Listen()
        {
            while (true)
            {
                Console.Write(" > ");
                string line = Console.ReadLine();

                if (line == "quit")
                    break;

                if (line.StartsWith("send;"))
                {
                    Commands.SendDataBlock(line);
                }

            }
        }
    }

    class Commands
    {
        public static void SendDataBlock(string command)
        {
            string[] data = command.Split(';');
            DataBlock dataBlock = new DataBlock(1,0,ushort.Parse(data[1]),data[2]);
            NetworkManager.GetInstance().SendToAllClients(dataBlock);
        }
    }
}
