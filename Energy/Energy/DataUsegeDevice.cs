using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Energy.Forms
{
    public partial class DataUsegeDevice : PrintDevice
    {
        Label label = new Label();
        private bool isFullTrafic = true;
        private bool isDay;

        public DataUsegeDevice()
        {
            isDay = DateTime.Now.Hour > 7 && DateTime.Now.Hour < 22;
            InitializeComponent();
            Initiatialize();
        }

        private void Initiatialize()
        {
            if (!isDay)
                this.BackColor = Color.FromArgb(62, 71, 88);
            this.Text = "Мой Дом";
            this.Size = new Size(1500, 750);
            label.TextAlign = ContentAlignment.MiddleCenter;
            label.Font = new Font("Arial", 22);
            label.Location = new Point(698, 248);
            label.Size = new Size(104, 104);
            this.Controls.Add(label);
        }

        protected override double GetTarif(List<Device> devices)
        {
            var tarif = new EkonomikInfo(devices, DateTime.MinValue);
            if (isFullTrafic)
                return tarif.StandartUsege;
            if (isDay)
                return tarif.DayUsage;
            return tarif.NightUsage;
        }

        private void DataUsegeDevice_Paint(object sender, PaintEventArgs e)
        {
            CreateNameDevice(e, 600, " кВт*ч");
            Bitmap myBitmap = Draws(200, 200, this, label, " кВт*ч");
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
