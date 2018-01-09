using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using Ether.Network;

namespace VictoriaShared.Networking
{
    public abstract class DataBlockController
    {
        private Dictionary<ushort, Action<DataBlock, NetConnection>> datablockMap = new Dictionary<ushort, Action<DataBlock, NetConnection>>();

        public DataBlockController()
        {
            datablockMap.Add(0, Admin);
            datablockMap.Add(1, ActionRequest);
            datablockMap.Add(2, Action);
        }

        public void ProcessDataBlock(DataBlock dataBlock, NetConnection netConnection)
        {
            datablockMap[dataBlock.type].Invoke(dataBlock, netConnection);
        }

        protected abstract void Admin(DataBlock dataBlock, NetConnection netConnection);
        protected abstract void ActionRequest(DataBlock dataBlock, NetConnection netConnection);
        protected abstract void Action(DataBlock dataBlock, NetConnection netConnection);
    }
}