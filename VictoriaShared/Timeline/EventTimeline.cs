using VictoriaShared.Networking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VictoriaShared.Timeline
{
    public class EventTimeline : Timeline<List<DataBlock>>
    {
        private long lastTime = 0;

        public EventTimeline(long initialTime)
        {
            lastTime = initialTime;
        }

        public void Add(long time, DataBlock value)
        {
            if (!data.ContainsKey(time))
            {
                data.Add(time, new List<DataBlock>());
            }

            data[time].Add(value);
        }

        public override List<DataBlock> Get(long time)
        {
            List<DataBlock> combined = new List<DataBlock>();
            for (long i = lastTime; i < time; i++)
            {
                if (!data.ContainsKey(time))
                    continue;

                foreach(DataBlock db in data[time])
                {
                    combined.Add(db);
                }
            }
            lastTime = time;
            return combined;
        }
    }
}
