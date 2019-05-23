using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CFD_program
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            Calculator1 cal = new Calculator1
            {
                Dx = double.Parse(dLinput.Text),
                Dy = double.Parse(dWinput.Text),
                L = double.Parse(Linput.Text),
                W = double.Parse(Winput.Text)
            };
            cal.SolveProblem();

            Graphics g = pictureBox1.CreateGraphics();
            g.Clear(Color.White);
            double Scale = double.Parse(Scaleinput.Text);
            double PlotSizeX = cal.L * 200 * Scale;
            double PlotSizeY = cal.W * 200 * Scale;
            
            g.DrawRectangle(Pens.Black, new Rectangle(0, 0, (int)PlotSizeX, (int)PlotSizeY));
            Contour con = new Contour(cal.Psi, cal.Dx, cal.Dy, PlotSizeX, PlotSizeY, g, int.Parse(Numinput.Text));
            con.DrawContourLines();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            Calculator2 cal = new Calculator2
            {
                H = 0.1,
                U = 1,
                L = 1
            };
            cal.SolveProblem();

            Graphics g = pictureBox2.CreateGraphics();
            g.Clear(Color.White);
            double Scale = double.Parse(Scaleinput.Text);
            double PlotSizeX = cal.L * 200 * Scale;
            double PlotSizeY = cal.L * 200 * Scale;

            g.DrawRectangle(Pens.Black, new Rectangle(0, 0, (int)PlotSizeX, (int)PlotSizeY));
            Contour con = new Contour(cal.Psi, cal.H, cal.H, PlotSizeX, PlotSizeY, g, 10);
            con.DrawContourLines();
        }
    }
}
