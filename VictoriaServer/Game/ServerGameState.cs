using SharpLogger;
using VictoriaShared.Game;
using VictoriaShared.Game.NetworkObjects;

namespace VictoriaServer.Game
{
    class ServerGameState : GameState
    {
        private static ServerGameState _instance;
        public static ServerGameState GetInstance()
        {
            if (_instance == null)
                _instance = new ServerGameState();

            return _instance;
        }
        private ServerGameState()
        {
            Initialize();
        }

        protected override void NetworkObjectAdded(NetworkObject networkObject)
        {
            Logger.Log(LogLevel.L2_Info, "Calling GameManager to spawn object.", "GameState");
        }

        protected override void NetworkObjectRemoved(NetworkObject networkObject)
        {
            Logger.Log(LogLevel.L2_Info, "Calling GameManager to destroy object.", "GameState");
        }
    }
}
