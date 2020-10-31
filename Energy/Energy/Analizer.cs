using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Energy
{
    public class Analizer
    {
        private static Tick prevTick;
        public static void Update(Tick tick)
        {
            if (prevTick is null)
            {
                tick.devices.First().SumUsege = tick.Usege;
            }
            else
            {
                var t = tick - prevTick;// t = Tuple<double, Device>
                if (t.Item2 is null)
                {
                    foreach(var d in tick.devices)
                        if (d.diapazon.IsIn(t.Item1))
                        {
                            d.SumUsege += t.Item1;
                        }
                }
                else t.Item2.SumUsege += t.Item1;
            }
            prevTick = new Tick(tick);
        }
    }
}
