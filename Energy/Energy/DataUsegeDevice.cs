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
using System.Drawing.Imaging;
using NUnit.Framework.Constraints;

namespace Energy.Forms
{
    public partial class DataUsegeDevice : Form
    {
        Label label = new Label();
        private List<Device> printDevice;
        private Dictionary<Type, Color> brush2;
        private bool isFullTrafic = true;
        private bool isDay;
        private bool isFirst = true;
        private Dictionary<Type, Label> PrintNameDevice;

        public DataUsegeDevice()
        {
            isDay = DateTime.Now.Hour > 7 && DateTime.Now.Hour < 22;
            InitializeComponent();
            Initiatialize();
        }

        private void Initiatialize()
        {
            PrintNameDevice = new Dictionary<Type, Label>();
            if (!isDay)
                this.BackColor = Color.FromArgb(62, 71, 88);
            this.Text = "Мой Дом";
            this.Size = new Size(1500, 750);
            printDevice = Manager.GetDevicesData().ToList();
            CreateBrush();
            this.Controls.Add(label);
        }


        private void CreateNameDevice(PaintEventArgs e)
        {
            var g = e.Graphics;
            var yPos = 500;
            var dy = 25;
            var xPos = 600;
            var dx = 200;
            foreach (var element in printDevice)
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
                        v.Substring(0, Math.Min(v.Length, 5)) + " кВт⋅ч";
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

        private void CreateBrush()
        {
            brush2 = new Dictionary<Type, Color>();
            brush2[Type.Computer] = Color.Yellow;
            brush2[Type.Condition] = Color.Red;
            brush2[Type.DishWasher] = Color.Blue;
            brush2[Type.Freese] = Color.Green;
            brush2[Type.Lamp] = Color.Pink;
        }

        public Bitmap Draws(int width, int height)
        {
            // Создаем новый образ и стираем фон
            Bitmap mybit = new Bitmap(width, height, PixelFormat.Format32bppArgb);
            Graphics graphics = Graphics.FromImage(mybit);
            SolidBrush brush = new SolidBrush(this.BackColor);
            graphics.FillRectangle(brush, 0, 0, width, height);
            brush.Dispose();

            var all = GetTarif(printDevice);
            var print = Manager.GetDevicesData().ToList();
            
            // Рисуем круговую диаграмму
            var startZ = 0.0f;
            var endZ = 0.0f;
            var current = 0.0;
            foreach (var e in Manager.GetDevicesData().ToList())
            {
                current += GetTarif(new List<Device> { e });
                startZ = endZ;
                endZ = (float)(current / all) * 360.0f;
                if (brush2.ContainsKey(e.TypeDevice))
                    graphics.FillPie(new SolidBrush(brush2[e.TypeDevice]), 0.0f, 0.0f, width, height, startZ, endZ - startZ);
            }
            label.Location = new Point(698, 248);
            label.Size = new Size(104, 104);
            label.Text = all.ToString().Substring(0, Math.Min(all.ToString().Length, 5)) + " кВт⋅ч";
            label.TextAlign = ContentAlignment.MiddleCenter;
            label.Font = new Font("Arial", 22);
            return mybit;
        }

        private double GetTarif(List<Device> devices)
        {
            var tarif = new EkonomikInfo(devices, DateTime.MinValue);
            if (isFullTrafic)
                return  tarif.StandartUsege;
            if (isDay)
                return tarif.DayUsage;
            return tarif.NightUsage;
        }

        private void DataUsegeDevice_Paint(object sender, PaintEventArgs e)
        {
            CreateNameDevice(e);
            Bitmap myBitmap = Draws(200, 200);
            Graphics g = e.Graphics;
            g.DrawImage(myBitmap, 650, 200);
            g.FillEllipse(new SolidBrush(this.BackColor), 675, 225, 150, 150);
        }

        private void DataUsegeDevice_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            var myButtom = (Button)sender;
            if (isFullTrafic)
            {  
                this.BackColor = Color.FromArgb(255, 255, 254);
                if (isDay)
                    myButtom.Text = "Day";
                else
                    myButtom.Text = "Night";
                isFullTrafic = false;
            }
            else
            {
                this.BackColor = Color.FromArgb(255, 255, 253);
                myButtom.Text = "За все время";
                isFullTrafic = true;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var myButtom = (Button)sender;

            if (!isFullTrafic)
                if (isDay)
                {
                    this.BackColor = Color.FromArgb(62, 71, 88);
                    myButtom.Text = "Night";
                    isDay = false;
                }
                else
                {   
                    this.BackColor = Color.White;
                    myButtom.Text = "Day";
                    isDay = true;
                }
        }
    }
}
