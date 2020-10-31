using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Energy
{
    public class EkonomikInfo
    {
        public double StandartTarif;
        public double DayTarif;
        public double NightTarif;
        public double StandartUsege;
        public double DayUsage;
        public double NightUsage;
        public EkonomikInfo(List<Device> devices, DateTime start, double tarif = 4.28,
            double dayTarif = 4.9, double nightTarif = 2.31)
        {
            //обычный тариф
            var sum = 0.0;
            foreach (var d in devices)
                sum += GetUsege(d, start) / d.GetGraf().Count;
            StandartTarif = sum * tarif;
            StandartUsege = sum;
            //день
            var daySum = 0.0;
            foreach (var d in devices)
                daySum += GetUsege(d, start, new DateTime(1, 1, 1, 7, 0, 0),
                    new DateTime(1, 1, 1, 23, 0, 0));
            DayTarif = daySum * dayTarif;
            DayUsage = daySum;
            //ночь
            NightTarif = (sum - daySum) * nightTarif;
            NightUsage = sum - daySum;
        }

        private double GetUsege(Device d, DateTime start)
        {
            var sum = 0.0;
            var list = d.GetGraf().Where(x => x.Item1 >= start).ToList();
            foreach(var t in list)
                sum += t.Item2;
            return sum;
        }

        private double GetUsege(Device d, DateTime start, DateTime a, DateTime b)
        {
            var sum = 0.0;
            var list = d.GetGraf()
                .Where(x => x.Item1 >= start)
                .Where(x =>
                {
                    TimeSpan timeOfDay = x.Item1.TimeOfDay;
                    return a.TimeOfDay < timeOfDay && timeOfDay < b.TimeOfDay;
                })
                .ToList();
            foreach (var t in list)
                sum += t.Item2;
            return sum;
        }
    }
}
