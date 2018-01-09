using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VictoriaShared.Timeline;

namespace VictoriaShared.Game.NetworkObjects
{
    public interface ITimelineTransform
    {
        LinearTimeline TimelinePositionX { get; }
        LinearTimeline TimelinePositionY { get; }
        LinearTimeline TimelineRotation { get; }

        void UpdateTimelineTransform();
    }
}
