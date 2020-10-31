using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Energy
{
    public class Tick
    {
        public List<Device> devices;
        public double Usege;

        public Tick(Tick tick)
        {
            Usege = tick.Usege;
            var res = new List<Device>();
            foreach(var d in tick.devices)
            {
                res.Add(new Device(d.TypeDevice, d.Count));
            }
            devices = res;
        }

        public Tick(string str, double d)
        {
            devices = str.Split(new string[] { ", " }, StringSplitOptions.RemoveEmptyEntries)
                .Select(x => x.Split(' '))
                .Select(x => Tuple.Create((Type)Enum.Parse(typeof(Type), x[0]), int.Parse(x[1])))
                .Select(x => new Device(x.Item1, x.Item2))
                .ToList();
            Usege = d;
        }

        public static Tuple<double, Device> operator -(Tick tick1, Tick tick2)
        {
            var list = new List<Device>();
            foreach(var e2 in tick1.devices)
            {
                var item = tick2.devices.Where(e1 => e1.Equals(e2)).FirstOrDefault();
                if (item is null)
                    return Tuple.Create(tick1.Usege - tick2.Usege, e2);
                if (e2.Count != item.Count)
                    return Tuple.Create(tick1.Usege - tick2.Usege, e2);
            }
            Device device = null;
            return Tuple.Create(tick1.Usege - tick2.Usege, device);
        }
    }
}
