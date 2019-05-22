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
                Dx = 0.025,
                Dy = 0.025,
                L = 3.5,
                W = 2
            };
            cal.SolveProblem();
            Graphics g = pictureBox1.CreateGraphics();
            Rectangle Border = new Rectangle(0, 0, 700, 400);
            g.DrawRectangle(Pens.Black, Border);
            Contour con = new Contour(cal.Psi, cal.Dx, cal.Dy, 700, 400, g, 20);
            con.DrawContourLines();
        }
    }
}
