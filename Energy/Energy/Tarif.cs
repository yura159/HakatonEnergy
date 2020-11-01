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
    public partial class Tarif : PrintDevice
    {
        Label label = new Label();

        public Tarif()
        {
            InitializeComponent();
            Initiatialize();
        }

        private void Initiatialize()
        {
            Paint += (a, e) => Tarif_Paint(e);
            this.Text = "Мой Дом";
            label.Location = new Point(148, 248);
            label.Size = new Size(104, 104);
            label.TextAlign = ContentAlignment.MiddleCenter;
            label.Font = new Font("Arial", 22);
            this.Controls.Add(label);
        }

        private void Tarif_Paint(PaintEventArgs e)
        {
            CreateNameDevice(e, 100, " руб");
            Bitmap myBitmap = Draws(200, 200, this, label, " руб");
            Graphics g = e.Graphics;
            g.DrawImage(myBitmap, 100, 200);
            g.FillEllipse(new SolidBrush(this.BackColor), 125, 225, 150, 150);
        }
    }
}
