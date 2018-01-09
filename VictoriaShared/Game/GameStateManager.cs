using VictoriaShared.Game.NetworkObjects;
using VictoriaShared.Networking;
using VictoriaShared.Timeline;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VictoriaShared.Game
{
    public abstract class GameStateManager
    {
        protected static GameStateManager instance = null;

        protected Dictionary<uint, NetworkObject> networkObjects;
        protected long timelineTime;
        protected EventTimeline eventTimeline;

        protected abstract void AddNetworkObject(NetworkObject networkObject);
        protected abstract void RemoveNetworkObject(uint id);
        public NetworkObject GetNetworkObject(uint id)
        {
            return this.networkObjects[id];
        }

        public long GetTimelineTime()
        {
            return timelineTime;
        }

        public void Update()
        {
            // -- Get Events
            List<DataBlock> events = eventTimeline.Get(timelineTime);

            // -- Process Events
            foreach(DataBlock evt in events)
            {
                // -- Process
                string[] data = evt.body.Split('#');
                DatablockProcess_AE(data, evt);
            }

            // -- Update Network Objects
            foreach(NetworkObject no in networkObjects.Values)
            {
                no.Update();
            }
        }

        public void AddDatablock(DataBlock dataBlock)
        {
            // -- Decode and apply to timeline
            string[] data = dataBlock.body.Split('#');

            // -- Category
            if (data[0] == "AE")
            {
                // -- Time
                long eventTime = long.Parse(data[1]);

                // -- Add to timeline
                eventTimeline.Add(eventTime, dataBlock);

                // -- Check for miss
                if (eventTime <= timelineTime)
                {
                    DatablockProcess_AE(data, dataBlock);
                }
            }
            else if (data[0] == "A")
            {
                DatablockProcess_A(data, dataBlock);
            }
        }

        private void DatablockProcess_AE(string[] data, DataBlock dataBlock)
        {
            // -- Time
            long eventTime = long.Parse(data[1]);

            // -- ReqId
            uint reqId = uint.Parse(data[2]);

            // -- Command
            string command = data[3];

            // If Spawn in place
            if(command == "spnp")
            {
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
                AddNetworkObject(networkObject);
            }
            
        }

        private void DatablockProcess_A(string[] data, DataBlock dataBlock)
        {

        }

    }
}
