using VictoriaShared.Game.NetworkObjects;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VictoriaShared.Timeline
{
    public abstract class Timeline<T>
    {
        protected SortedList<long, T> data = new SortedList<long, T>();

        public virtual void Add(long time, T value)
        {
            data.Add(time, value);
        }

        public abstract T Get(long time);

        public SortedList<long, T> GetSortedList()
        {
            return data;
        }
    }
}
