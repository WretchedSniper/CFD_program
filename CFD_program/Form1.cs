using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FlowAroundCylinder
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
                Dx = 0.25,
                Dy = 0.25,
                L = 3.5,
                W = 2
            };
            cal.SolveProblem();
            Graphics g = pictureBox1.CreateGraphics();
            Rectangle Border = new Rectangle(10, 10, 700, 400);
            g.DrawRectangle(Pens.Black, Border);
        }
    }
}
