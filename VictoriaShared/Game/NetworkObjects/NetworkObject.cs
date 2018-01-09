using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VictoriaShared.Game.NetworkObjects
{
    public abstract class NetworkObject
    {
        public uint ID { get; }

        public NetworkObject(uint id)
        {
            this.ID = id;
        }

        public abstract void Update();
    }
}
