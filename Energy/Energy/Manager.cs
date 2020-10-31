using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Energy
{
    public class Manager
    {
        public static void Update(InputData inputData)
        {
            var data = inputData.GetValue();
            if (!(data is null))
            {
                Statistic.UpdateTick(data);
            }

        }

        public static List<Device> GetDevicesData() => Statistic.GetDevices();
    }
}
