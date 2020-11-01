using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Energy
{
    public partial class Tarif : Form
    {

        Label label = new Label();
        private List<Device> printDevice;
        private Dictionary<Type, Color> brush2;
        private bool isFirst = true;
        private Dictionary<Type, Label> PrintNameDevice;

        public Tarif()
        {
            InitializeComponent();
            Initiatialize();
        }

        private void Initiatialize()
        {
            Paint += (a, e) => Tarif_Paint(e);
            PrintNameDevice = new Dictionary<Type, Label>();
            this.Text = "Мой Дом";
            printDevice = Manager.GetDevicesData().ToList();
            brush2 = Device.CreateBrush();
            label.Location = new Point(148, 248);
            label.Size = new Size(104, 104);
            label.TextAlign = ContentAlignment.MiddleCenter;
            label.Font = new Font("Arial", 22);
            this.Controls.Add(label);
        }


        private void CreateNameDevice(PaintEventArgs e)
        {
            var g = e.Graphics;
            var yPos = 500;
            var dy = 25;
            var xPos = 100;
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
                        v.Substring(0, Math.Min(v.Length, 5)) + " руб";
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

        private double GetTarif(List<Device> devices)
        {
            return new EkonomikInfo(devices, DateTime.MinValue).StandartTarif;
        }

        private void Tarif_Paint(PaintEventArgs e)
        {
            CreateNameDevice(e);
            Bitmap myBitmap = PrintDevice.Draws(200, 200, this, label, GetTarif, " руб");
            Graphics g = e.Graphics;
            g.DrawImage(myBitmap, 100, 200);
            g.FillEllipse(new SolidBrush(this.BackColor), 125, 225, 150, 150);
        }
    }
}
