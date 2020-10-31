using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Windows.Forms.DataVisualization.Charting;
using System.Drawing.Imaging;
using NUnit.Framework.Constraints;

namespace Energy.Forms
{
    public partial class DataUsegeDevice : Form
    {
        private List<Device> printDevice;
        private Dictionary<Type, Color> brush2;

        public DataUsegeDevice()
        {

            Initiatialize();
            InitializeComponent();
        }

        private void Initiatialize()
        {
            this.Text = "Мой Дом";
            this.Size = new Size(1500, 750);
            printDevice = Manager.GetDevicesData().ToList();
            CreateBrush();
        }


        private void CreateNameDevice(object sender, PaintEventArgs e)
        {
            var g = e.Graphics;
            var yPos = 500;
            var dy = 25;
            var xPos = 600;
            var dx = 100;
            foreach(var element in printDevice)
            {
                var text = new Label();
                text.Location = new Point(xPos, yPos);
                g.FillRectangle(new SolidBrush(brush2[element.TypeDevice]), new Rectangle(xPos + dx, yPos, dx, dy));
                text.Size = new Size(dx, dy);
                text.Font = new Font("Arial", 12);
                yPos += dy;
                if (yPos + 2 * dy >= this.Size.Height)
                {
                    yPos = 500;
                    xPos += dx;
                }
                text.Text = element.TypeDevice.ToString();
                this.Controls.Add(text);
            }
        }

        private void CreateBrush()
        {
            brush2 = new Dictionary<Type, Color>();
            brush2[Type.Computer] = Color.Yellow;
            brush2[Type.Condition] = Color.Red;
            brush2[Type.DishWasher] = Color.Blue;
            brush2[Type.Freese] = Color.Green;
            brush2[Type.Lamp] = Color.Pink;
        }

        public Bitmap Draws(Color bgCol, int width, int height)
        {
            // Создаем новый образ и стираем фон
            Bitmap mybit = new Bitmap(width, height, PixelFormat.Format32bppArgb);
            Graphics graphics = Graphics.FromImage(mybit);
            SolidBrush brush = new SolidBrush(bgCol);
            graphics.FillRectangle(brush, 0, 0, width, height);
            brush.Dispose();

            // Сумма для получения общего
            var all = printDevice.Select(e => e.SumUsege).Sum();
            var print = Manager.GetDevicesData().ToList();
            // Рисуем круговую диаграмму
            var startZ = 0.0f;
            var endZ = 0.0f;
            var current = 0.0;
            foreach (var e in print)
            {
                current += e.SumUsege;
                startZ = endZ;
                endZ = (float)(current / all) * 360.0f;
                if (brush2.ContainsKey(e.TypeDevice))
                    graphics.FillPie(new SolidBrush(brush2[e.TypeDevice]), 0.0f, 0.0f, width, height, startZ, endZ - startZ);
            }

            return mybit;
        }

        private void DataUsegeDevice_Paint(object sender, PaintEventArgs e)
        {
            CreateNameDevice(sender, e);
            var myColor = Color.FromArgb(255, 255, 255);
            Bitmap myBitmap = Draws(myColor, 200, 200);
            Graphics g = e.Graphics;
            g.DrawImage(myBitmap, 650, 200);
            g.FillEllipse(new SolidBrush(Color.White), 675, 225, 150, 150);
        }

        private void DataUsegeDevice_Load(object sender, EventArgs e)
        {

        }
    }
}
