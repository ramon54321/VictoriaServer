using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VictoriaShared.Timeline;

namespace VictoriaShared.Game.NetworkObjects
{
    public class Unit : NetworkObject, ITimelineTransform
    {
        public Unit(uint id) : base(id)
        {
            TimelinePositionX = new LinearTimeline();
            TimelinePositionY = new LinearTimeline();
            TimelineRotation = new LinearTimeline();
        }

        public LinearTimeline TimelinePositionX { get; }

        public LinearTimeline TimelinePositionY { get; }

        public LinearTimeline TimelineRotation { get; }

        public override void Update()
        {
            //UpdateTimelineTransform();
        }

        public void UpdateTimelineTransform()
        {
            throw new NotImplementedException();
        }
    }
}
