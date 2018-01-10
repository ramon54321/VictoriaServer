using System;
using System.Collections.Generic;
using System.Text;
using SharpLogger;
using VictoriaShared.Game;
using VictoriaShared.Game.NetworkObjects;
using VictoriaShared.Timeline;

namespace VictoriaServer.Game
{
    class ServerGameState : GameState
    {
        public static ServerGameState GetInstance()
        {
            if (Instance == null)
                Instance = new ServerGameState();

            return (ServerGameState)Instance;
        }
        private ServerGameState()
        {
            NetworkObjects = new Dictionary<uint, NetworkObject>();
            TimelineTime = DateTimeOffset.Now.ToUnixTimeMilliseconds();
            EventTimeline = new EventTimeline(TimelineTime);

            Logger.Log(LogLevel.L2_Info, "Game state manager instantiated at time: " + TimelineTime + ".", "Singletons.GameState");
        }

        protected override void AddNetworkObject(NetworkObject networkObject)
        {
            throw new NotImplementedException();
        }

        protected override void RemoveNetworkObject(uint id)
        {
            throw new NotImplementedException();
        }
    }
}
