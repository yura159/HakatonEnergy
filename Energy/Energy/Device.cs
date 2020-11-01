using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Energy
{
    public enum Type
    {
        Computer,
        Lamp,
        Freese,
        Condition,
        DishWasher
    }

    public class Device
    {   // содержит информацию о типе устройства
        public Diapazon diapazon;
        public int Count;
        public double SumUsege;
        public Type TypeDevice;
        public Color color;


        List<Tuple<DateTime, double>> graf = new List<Tuple<DateTime, double>>();

        public void UpdateGraf()
        {
            graf.Add(Tuple.Create(DateTime.Now, SumUsege));
        }

        public List<Tuple<DateTime, double>> GetGraf()
        {
            return graf;
        }

        public static Dictionary<Type, Color> CreateBrush()
        {
            var brush2 = new Dictionary<Type, Color>();
            brush2[Type.Computer] = Color.Yellow;
            brush2[Type.Condition] = Color.Red;
            brush2[Type.DishWasher] = Color.Blue;
            brush2[Type.Freese] = Color.Green;
            brush2[Type.Lamp] = Color.Pink;
            return brush2;
        }
        public Device(Type type, int c)
        {
            TypeDevice = type;
            Count = c;
            SetDiapazon();
        }

        public override bool Equals(object obj)
        {
            if (obj is Device)
            {
                return TypeDevice == ((Device)obj).TypeDevice;
            }
            return false;
        }

        private void SetDiapazon()
        {
            switch (TypeDevice)
            {
                case Type.Computer:
                    diapazon = new Diapazon(0, 0.001);
                    color = Color.Yellow;
                    break;
                case Type.Freese:
                    diapazon = new Diapazon(0.07, 0.1);
                    color = Color.Green;
                    break;
                case Type.Lamp:
                    diapazon = new Diapazon(0.001, 0.03);
                    color = Color.Pink;
                    break;
                case Type.Condition:
                    diapazon = new Diapazon(0.03, 0.07);
                    color = Color.Red;
                    break;
                case Type.DishWasher:
                    diapazon = new Diapazon(0.1, 0.2);
                    color = Color.Blue;
                    break;
            }
        }
    }

    public class Diapazon
    {
        private double a, b;
        public Diapazon(double a, double b)
        {
            this.a = a;
            this.b = b;
        }

        public bool IsIn(double d)
        {
            d = Math.Abs(d);
            return a <= d && d < b;
        }
    }
}
