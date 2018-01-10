using System;
using System.Threading;
using VictoriaServer.Networking;
using VictoriaServer.Admin;
using VictoriaServer.Game;
using VictoriaShared.Game;
using VictoriaShared.Networking;

namespace VictoriaServer
{
    class Program
    {
        static void Main(string[] args)
        {
            Test();

            Console.Title = "Project Victoria Server";
            Console.WriteLine("\n --- Server Started --- \n");

            // -- Set up networking
            Server server = new Server();

            Thread networkThread = new Thread(delegate ()
            {
                server.Start();
            });
            networkThread.Name = "Network Thread";
            networkThread.Start();

            // -- Set up gamestate
            GameState gameState = ServerGameState.GetInstance();
            

            // -- Command loop
            CommandManager commandManager = new CommandManager();

            // -- Close down
            server.Stop();
        }

        private static void Test()
        {

        }
    }
}
