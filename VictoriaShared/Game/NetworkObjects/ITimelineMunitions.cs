using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VictoriaShared.Timeline;

namespace VictoriaShared.Game.NetworkObjects
{
    public interface ITimelineMunitions
    {
        StepTimeline TimelineMunitions { get; }

        void UpdateTimelineMunitions();
    }
}
