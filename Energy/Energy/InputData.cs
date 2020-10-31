using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Energy
{
    public class InputData
    {
        private List<Tick> ticks;

        public InputData(List<Tick> ticks)
        {
            this.ticks = ticks;
        }

        public Tick GetValue()
        {
            var res = ticks.FirstOrDefault();
            if(res != null) ticks.RemoveAt(0);
            return res;
        }
    }
}
