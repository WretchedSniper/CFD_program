using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFD_program
{
    class Calculator2
    {
        public double H { get; set; }
        public double U { get; set; }
        public double L { get; set; }
        public int M { get; private set; }
        public double[,] Psi { get; private set; }
        public double[,] Zeta { get; private set; }
        private double[,] lastPsi, innerlastPsi;
        private double[,] lastZeta;
        private double norm;
        public void SolveProblem()
        {
            M = Convert.ToInt32(L / H + 1);
            Psi = new double[M, M];
            Zeta = new double[M, M];
            lastZeta = new double[M, M];
            lastPsi = new double[M, M];
            innerlastPsi = new double[M, M];

            while (true)
            {
                Array.Copy(Psi, lastPsi, Psi.Length);
                for (int i = 1; i < M - 1; ++i)
                {
                    Zeta[i, 0] = (-Psi[i - 1, 1] + 8 * Psi[i, 1] / 3 - Psi[i + 1, 1] - 2 * Psi[i, 2] / 3) / (H * H) + 2 * U / (3 * H);
                    Zeta[i, M - 1] = 0;
                    Zeta[0, i] = (-Psi[1, i - 1] + 8 * Psi[1, i] / 3 - Psi[1, i + 1] - 2 * Psi[2, i] / 3) / (H * H);
                    Zeta[M - 1, i] = (-Psi[M - 2, i - 1] + 8 * Psi[M - 2, i] / 3 - Psi[M - 2, i + 1] - 2 * Psi[M - 3, i] / 3) / (H * H);
                }
                Zeta[0, 0] = (Zeta[0, 1] + Zeta[1, 0]) / 2;
                Zeta[0, M - 1] = (Zeta[0, M - 2] + Zeta[1, M - 1]) / 2;
                Zeta[M - 1, 0] = (Zeta[M - 1, 1] + Zeta[M - 2, 0]) / 2;
                Zeta[M - 1, M - 1] = (Zeta[M - 2, M - 1] + Zeta[M - 1, M - 2]) / 2;

                while (true)
                {
                    Array.Copy(Zeta, lastZeta, Zeta.Length);
                    for (int i = 1; i < M - 1; ++i)
                        for (int j = 1; j < M - 1; ++j)
                            Zeta[i, j] = (Zeta[i - 1, j] + Zeta[i + 1, j] + Zeta[i, j - 1] + Zeta[i, j + 1]) / 4;

                    norm = 0;
                    for (int i = 0; i < M; ++i)
                        for (int j = 0; j < M; ++j)
                        {
                            lastZeta[i, j] = Zeta[i, j] - lastZeta[i, j];
                            if (Math.Abs(lastZeta[i, j]) > norm)
                                norm = Math.Abs(lastZeta[i, j]);
                        }
                    if (norm < 0.0001)
                        break;
                }

                while (true)
                {
                    Array.Copy(Psi, innerlastPsi, Psi.Length);
                    for (int i = 1; i < M - 1; ++i)
                        for (int j = 1; j < M - 1; ++j)
                            Psi[i, j] = (Zeta[i, j] * (H * H) + Psi[i - 1, j] + Psi[i + 1, j] + Psi[i, j - 1] + Psi[i, j + 1]) / 4;

                    norm = 0;
                    for (int i = 0; i < M; ++i)
                        for (int j = 0; j < M; ++j)
                        {
                            innerlastPsi[i, j] = Psi[i, j] - innerlastPsi[i, j];
                            if (Math.Abs(innerlastPsi[i, j]) > norm)
                                norm = Math.Abs(innerlastPsi[i, j]);
                        }
                    if (norm < 0.0001)
                        break;
                }

                norm = 0;
                for (int i = 0; i < M; ++i)
                    for (int j = 0; j < M; ++j)
                    {
                        lastPsi[i, j] = Psi[i, j] - lastPsi[i, j];
                        if (Math.Abs(lastPsi[i, j]) > norm)
                            norm = Math.Abs(lastPsi[i, j]);
                    }
                if (norm < 0.0001)
                    break;
            }
        }
    }
}