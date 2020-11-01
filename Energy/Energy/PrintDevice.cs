using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace Energy
{
    public class PrintDevice : Form
    {
        private Dictionary<Type, Color> brush2 = Device.CreateBrush();
        private bool isFirst = true;
        private Dictionary<Type, Label> PrintNameDevice  = new Dictionary<Type, Label>();

        public void CreateNameDevice(PaintEventArgs e, int xPos, string name)
        {
            var g = e.Graphics;
            var yPos = 500;
            var dy = 25;
            var dx = 200;
            foreach (var element in Manager.GetDevicesData())
            {
                g.FillRectangle(new SolidBrush(brush2[element.TypeDevice]), new Rectangle(xPos + dx, yPos, dx, dy));

                if (isFirst)
                {
                    var text = new Label();
                    text.Location = new Point(xPos, yPos);
                    text.Size = new Size(dx, dy);
                    text.Font = new Font("Arial", 12);
                    text.Text = element.TypeDevice.ToString() + " " + GetTarif(new List<Device> { element }).ToString() + " кВт⋅ч";
                    this.Controls.Add(text);
                    PrintNameDevice[element.TypeDevice] = text;

                }
                else
                {
                    var v = GetTarif(new List<Device> { element }).ToString();
                    PrintNameDevice[element.TypeDevice].Text = element.TypeDevice.ToString() + " " +
                        v.Substring(0, Math.Min(v.Length, 5)) + name;
                }
                yPos += dy;
                if (yPos + 2 * dy >= this.Size.Height)
                {
                    yPos = 500;
                    xPos += dx;
                }
            }
            isFirst = false;
        }
        public Bitmap Draws(int width, int height, Form form, Label label, string name)
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
        protected virtual double GetTarif(List<Device> devices)
        {
            return new EkonomikInfo(devices, DateTime.MinValue).StandartTarif;
        }
    }
}
