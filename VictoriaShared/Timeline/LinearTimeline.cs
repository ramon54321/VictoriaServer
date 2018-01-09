using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VictoriaShared.Timeline
{
    public class LinearTimeline : Timeline<float>
    {
        public override float Get(long time)
        {
            int index = data.Keys.BinarySearchIndexOf(time);
            if (index < 0)
            {
                index = ~index - 1;

                if (index < 0)
                    return data[data.Keys[0]];
            }

            if (index+1 >= data.Keys.Count)
            {
                return data[data.Keys[index]];
            }

            long timeA = data.Keys[index];
            long timeB = data.Keys[(index + 1)];
            float a = data[timeA];
            float b = data[timeB];
            float x = (float)(time - timeA) / (float)(timeB - timeA);

            return a + x * (b - a);
        }
    }
}
