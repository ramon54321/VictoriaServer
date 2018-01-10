using System;
using SharpLogger;
using VictoriaServer.Admin;
using VictoriaServer.Networking;
using VictoriaServer.Game;

namespace VictoriaServer
{
    class Program
    {
        private static NetworkManager _networkManager;
        private static ServerGameState _serverGameState;

        static void Main(string[] args)
        {
            Console.Title = "Project Victoria Server";
            Logger.SetPrinter(new Printer());
            Logger.SetFilter("");
            Logger.Log(LogLevel.L2_Info, "\n --- Server Started --- \n", "");

            // -- Set up Networking
            _networkManager = NetworkManager.GetInstance();

            // -- Set up GameState
            _serverGameState = ServerGameState.GetInstance();

            // -- Command loop -- Blocking!!!
            CommandManager commandManager = new CommandManager();

            // -- Close down
            _networkManager.CloseSocket();
        }
    }
}
