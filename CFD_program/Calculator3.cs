using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFD_program
{
    class Calculator3
    {
        public double Eta_max { get; set; }
        public double H { get; set; }
        public double[,] F { get; private set; }
        public double[,] G { get; private set; }
        public double Beta { get; private set; }
        private double z_max;
        private int z_length;
        private int eta_length;
        public void SolveProblem()
        {
            z_max = Eta_max * 10;
            z_length = Convert.ToInt32(z_max / H + 1);
            G = new double[z_length, 3];
            G[0, 0] = 0;
            G[0, 1] = 0;
            G[0, 2] = 1;
            for (int i = 0; i < z_length - 1; ++i)
            {
                double k11 = H * G[i, 1];
                double k21 = H * G[i, 2];
                double k31 = -H * G[i, 0] * G[i, 2] / 2;
                double k12 = H * (G[i, 1] + k21 / 2);
                double k22 = H * (G[i, 2] + k31 / 2);
                double k32 = -H * (G[i, 0] + k11 / 2) * (G[i, 2] + k31 / 2) / 2;
                double k13 = H * (G[i, 1] + k22 / 2);
                double k23 = H * (G[i, 2] + k32 / 2);
                double k33 = -H * (G[i, 0] + k12 / 2) * (G[i, 2] + k32 / 2) / 2;
                double k14 = H * (G[i, 1] + k23);
                double k24 = H * (G[i, 2] + k33);
                double k34 = -H * (G[i, 0] + k13) * (G[i, 2] + k33) / 2;
                G[i + 1, 0] = G[i, 0] + (k11 + 2 * k12 + 2 * k13 + k14) / 6;
                G[i + 1, 1] = G[i, 1] + (k21 + 2 * k22 + 2 * k23 + k24) / 6;
                G[i + 1, 2] = G[i, 2] + (k31 + 2 * k32 + 2 * k33 + k34) / 6;
            }
            Beta = Math.Sqrt(G[z_length - 1, 1]);

            eta_length = Convert.ToInt32(Eta_max / H + 1);
            F = new double[eta_length, 3];
            F[0, 0] = 0;
            F[0, 1] = 0;
            F[0, 2] = 1 / (Beta * Beta * Beta);
            for (int i = 0; i < eta_length - 1; ++i)
            {
                double k11 = H * F[i, 1];
                double k21 = H * F[i, 2];
                double k31 = -H * F[i, 0] * F[i, 2] / 2;
                double k12 = H * (F[i, 1] + k21 / 2);
                double k22 = H * (F[i, 2] + k31 / 2);
                double k32 = -H * (F[i, 0] + k11 / 2) * (F[i, 2] + k31 / 2) / 2;
                double k13 = H * (F[i, 1] + k22 / 2);
                double k23 = H * (F[i, 2] + k32 / 2);
                double k33 = -H * (F[i, 0] + k12 / 2) * (F[i, 2] + k32 / 2) / 2;
                double k14 = H * (F[i, 1] + k23);
                double k24 = H * (F[i, 2] + k33);
                double k34 = -H * (F[i, 0] + k13) * (F[i, 2] + k33) / 2;
                F[i + 1, 0] = F[i, 0] + (k11 + 2 * k12 + 2 * k13 + k14) / 6;
                F[i + 1, 1] = F[i, 1] + (k21 + 2 * k22 + 2 * k23 + k24) / 6;
                F[i + 1, 2] = F[i, 2] + (k31 + 2 * k32 + 2 * k33 + k34) / 6;
            }
        }
        public void Figure(double StartX, double StartY, double PlotSizeX, double PlotSizeY, Graphics g)
        {
            int Delta = 5;
            for (int i = 0; i < eta_length - 1; i += Delta)
            {
                float x1 = Convert.ToSingle(StartX + F[i, 1] * PlotSizeX);
                float y1 = Convert.ToSingle(StartY + PlotSizeY * (1 - i * H / Eta_max));
                float x2 = Convert.ToSingle(StartX + F[i + Delta, 1] * PlotSizeX);
                float y2 = Convert.ToSingle(StartY + PlotSizeY * (1 - (i + Delta) * H / Eta_max));
                float x3 = Convert.ToSingle(StartX + (i * H * F[i, 1] - F[i, 0]) * PlotSizeX / 2);
                float y3 = Convert.ToSingle(StartY + PlotSizeY * (1 - i * H / Eta_max));
                float x4 = Convert.ToSingle(StartX + ((i + Delta) * H * F[i + Delta, 1] - F[i + Delta, 0]) * PlotSizeX / 2);
                float y4 = Convert.ToSingle(StartY + PlotSizeY * (1 - (i + Delta) * H / Eta_max));
                PointF P1 = new PointF(x1, y1);
                PointF P2 = new PointF(x2, y2);
                PointF P3 = new PointF(x3, y3);
                PointF P4 = new PointF(x4, y4);
                g.DrawLine(new Pen(Color.Red, 3), P1, P2);
                g.DrawLine(new Pen(Color.Green, 3), P3, P4);
            }
        }
    }
}
