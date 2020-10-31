using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Energy
{
    public static class Statistic
    {
        static List<Device> devices = new List<Device>();



        public static List<Device> GetDevices() => devices;

        public static void UpdateTick(Tick tick)
        {
            foreach(var d in tick.devices)
                if (!devices.Contains(d))
                    devices.Add(d);
            var res = devices.ToList();
            foreach(var d in res)
            {
                var a = tick.devices.Where(x => x.Equals(d)).FirstOrDefault();
                if (a != null)
                    d.Count = a.Count;
                else
                    d.Count = 0;
            }
            tick.devices = res;
            Analizer.Update(tick);
            devices.ForEach(e => e.UpdateGraf());
        }
    }
}
