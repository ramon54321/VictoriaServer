using System;
using System.Collections.Generic;
using SharpLogger;
using VictoriaShared.Game.NetworkObjects;
using VictoriaShared.Networking;
using VictoriaShared.Timeline;

namespace VictoriaShared.Game
{
    public abstract class GameState
    {
        private long _timelineTime;

        private Dictionary<uint, NetworkObject> _networkObjects;
        private EventTimeline _eventTimeline;

        protected abstract void NetworkObjectAdded(NetworkObject networkObject);
        protected abstract void NetworkObjectRemoved(NetworkObject networkObject);

        public NetworkObject GetNetworkObject(uint id)
        {
            return _networkObjects[id];
        }
        public Dictionary<uint, NetworkObject>.Enumerator GetNetworkObjectsEnumerator()
        {
            return _networkObjects.GetEnumerator();
        }

        protected void Initialize()
        {
            _timelineTime = DateTimeOffset.Now.ToUnixTimeMilliseconds();
            _networkObjects = new Dictionary<uint, NetworkObject>();
            _eventTimeline = new EventTimeline(_timelineTime);
            Logger.Log(LogLevel.L2_Info, "GameState Initialized", "GameState");
        }
        public void Update()
        {
            // -- Update Time
            _timelineTime  = DateTimeOffset.Now.ToUnixTimeMilliseconds();

            // -- Get Events
            List<DataBlock> timelineEvents = _eventTimeline.Get(_timelineTime);

            // -- Process Events
            foreach(DataBlock timelineEvent in timelineEvents)
            {
                // -- Process
                string[] data = timelineEvent.body.Split('#');
                DatablockProcess(data, timelineEvent);
            }

            // -- Update Network Objects
            foreach(NetworkObject networkObject in _networkObjects.Values)
            {
                networkObject.Update();
            }
        }

        public void AddDatablockEvent(DataBlock dataBlock)
        {
            // -- Get parameters
            string[] data = dataBlock.body.Split('#');

            // -- Time
            long eventTime = long.Parse(data[1]);

            // -- Add to timeline
            _eventTimeline.Add(eventTime, dataBlock);

            // -- If missed, do immediately
            if (eventTime <= _timelineTime)
            {
                DatablockProcess(data, dataBlock);
            }
        }
        public void AddDatablockAction(DataBlock dataBlock)
        {
            // -- Get parameters
            string[] data = dataBlock.body.Split('#');

            // -- Process Action immediately
            DatablockProcess(data, dataBlock);
        }
        private void DatablockProcess(string[] data, DataBlock dataBlock)
        {
            // -- Function
            DataBlockFunction function = dataBlock.function;

            switch (function)
            {
                case DataBlockFunction.Event_4001_Spawn:
                    Logger.Log(LogLevel.L2_Info, "Event_4001_Spawn", "GameState.Function.Event");
                    long eventTime = long.Parse(data[1]);
                    uint reqId = uint.Parse(data[2]);
                    uint id = uint.Parse(data[4]);
                    string obj = data[5];
                    float x = float.Parse(data[6]);
                    float y = float.Parse(data[7]);
                    float rot = float.Parse(data[8]);
                    ushort player = ushort.Parse(data[9]);
                    float health = float.Parse(data[10]);
                    ushort munitions = ushort.Parse(data[11]);
                    float fuel = float.Parse(data[12]);

                    Unit networkObject = new Unit(id);
                    networkObject.TimelinePositionX.Add(eventTime, x);
                    networkObject.TimelinePositionY.Add(eventTime, y);
                    networkObject.TimelineRotation.Add(eventTime, rot);
                    NetworkObjectAdded(networkObject);
                    return;
            }
        }
    }
}
