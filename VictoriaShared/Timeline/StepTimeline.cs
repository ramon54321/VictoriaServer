using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VictoriaShared.Timeline
{
    public class StepTimeline : Timeline<short>
    {
        public override short Get(long time)
        {
            int index = data.Keys.BinarySearchIndexOf(time);
            if (index < 0)
            {
                index = ~index - 1;

                if (index < 0)
                    index = 0;
            }

            return data[data.Keys[index]];
        }
    }
}
