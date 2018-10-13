using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ImageTestWF
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var t = Screenshot();
        }

        private Bitmap Screenshot()
        {
            Bitmap screenshot = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
            Graphics g = Graphics.FromImage(screenshot);
            g.CopyFromScreen(0,0,0,0,Screen.PrimaryScreen.Bounds.Size);

            return screenshot;
        }

        private bool FindBitMap (Bitmap bmpnidle, Bitmap bmpheystack, out Point location)
        {
            for (int outerX=0;outerX<bmpheystack.Width-bmpnidle.Width; outerX++)
            {
                for (int outerY = 0; outerY < bmpheystack.Height - bmpnidle.Height; outerY++)
                {
                    for (int innerX = 0; innerX < bmpnidle.Width; innerX++)
                    {
                        for (int innerY = 0; innerY < bmpnidle.Height; innerY++)
                        {
                            Color cNeedle = bmpnidle.GetPixel(innerX,innerY);
                            Color cHeystack = bmpnidle.GetPixel(innerX+outerX, innerY+outerY);
                            if (cNeedle.R!=cHeystack.R||cNeedle.G!=cHeystack.G||cNeedle.B!=cHeystack.B)
                            {
                                goto notFound;
                            }
                        }
                    }
                    location = new Point(outerX, outerY);
                    return true;
                notFound:
                    continue;
                }
            }
            location = Point.Empty;
            return false;
        }
    }
}
