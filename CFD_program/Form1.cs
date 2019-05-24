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
            double StartX = (1000 - PlotSizeX) / 2;
            double StartY = (600 - PlotSizeY) / 2;
            
            g.DrawRectangle(Pens.Black, new Rectangle((int)StartX, (int)StartY, (int)PlotSizeX, (int)PlotSizeY));
            Contour con = new Contour(cal.Psi, cal.Dx, cal.Dy, StartX, StartY, PlotSizeX, PlotSizeY, g, int.Parse(Numinput.Text));
            con.DrawContourLines();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            Calculator2 cal = new Calculator2
            {
                H = double.Parse(Hinput2.Text),
                U = double.Parse(Uinput2.Text),
                L = double.Parse(Linput2.Text)
            };
            if (cal.L / cal.H > 12)
                MessageBox.Show("高斯-赛德尔迭代不收敛");
            else
            {
                cal.SolveProblem();

                Graphics g = pictureBox2.CreateGraphics();
                g.Clear(Color.White);
                double PlotSizeX = 580;
                double PlotSizeY = 580;
                double StartX = (1000 - PlotSizeX) / 2;
                double StartY = (600 - PlotSizeY) / 2;

                g.DrawRectangle(Pens.Black, new Rectangle((int)StartX, (int)StartY, (int)PlotSizeX, (int)PlotSizeY));
                Contour con = new Contour(cal.Psi, cal.H, cal.H, StartX, StartY, PlotSizeX, PlotSizeY, g, int.Parse(Numinput2.Text));
                con.DrawContourLines();
            }
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            Calculator3 cal = new Calculator3() { Eta_max = double.Parse(EtamaxInput3.Text), H = double.Parse(Hinput3.Text) };
            cal.SolveProblem();
            if (cal.H >0.05)
                MessageBox.Show("步长过大");
            else
            {
                Graphics g = pictureBox3.CreateGraphics();
                g.Clear(Color.White);
                double PlotSizeX = 900;
                double PlotSizeY = 500;
                double StartX = (1000 - PlotSizeX) / 2;
                double StartY = (600 - PlotSizeY) / 2;
                g.DrawRectangle(Pens.Black, new Rectangle((int)StartX, (int)StartY, (int)PlotSizeX, (int)PlotSizeY));
                cal.Figure(StartX, StartY, PlotSizeX, PlotSizeY, g);
            }
        }
    }
}
