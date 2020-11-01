using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Energy
{
    public class PrintDevice
    {

        private static Dictionary<Type, Color> brush2 = Device.CreateBrush();

        public static Bitmap Draws(int width, int height, Form form, Label label,
            Func<List<Device>, double> GetTarif, string name)
        {
            // Создаем новый образ и стираем фон
            Bitmap mybit = new Bitmap(width, height, PixelFormat.Format32bppArgb);
            Graphics graphics = Graphics.FromImage(mybit);
            SolidBrush brush = new SolidBrush(form.BackColor);
            graphics.FillRectangle(brush, 0, 0, width, height);
            brush.Dispose();

            var all = GetTarif(Manager.GetDevicesData());

            // Рисуем круговую диаграмму
            var startZ = 0.0f;
            var endZ = 0.0f;
            var current = 0.0;
            foreach (var e in Manager.GetDevicesData())
            {
                current += GetTarif(new List<Device> { e });
                startZ = endZ;
                endZ = (float)(current / all) * 360.0f;
                if (brush2.ContainsKey(e.TypeDevice))
                    graphics.FillPie(new SolidBrush(brush2[e.TypeDevice]), 0.0f, 0.0f, width, height, startZ, endZ - startZ);
            }
            label.Text = all.ToString().Substring(0, Math.Min(all.ToString().Length, 5)) + name;
            return mybit;
        }
    }
}
