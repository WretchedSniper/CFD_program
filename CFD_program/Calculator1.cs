using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFD_program
{
    class Calculator1
    {
        public double Dx { get; set; }
        public double Dy { get; set; }
        public double L { get; set; }
        public double W { get; set; }
        public int Nx { get; private set; }
        public int Ny { get; private set; }
        public double[,] Psi { get; private set; }
        public int K { get; private set; }
        private double[,] lastPsi;
        public void SolveProblem()
        {
            Nx = Convert.ToInt32(L / Dx + 1);
            Ny = Convert.ToInt32(W / Dy + 1);
            Psi = new double[Nx, Ny];
            lastPsi = new double[Nx, Ny];
            for (int i = 0; i < Nx; ++i)
                Psi[i, Ny - 1] = W;
            for (int j = 0; j < Ny; ++j)
                Psi[0, j] = j * Dy;
            K = 0;
            while (true)
            {
                ++K;
                Array.Copy(Psi, lastPsi, Psi.Length);
                for (int i = 1; i < Nx; ++i)
                    for (int j = 1; j < Ny - 1; ++j)
                    {
                        if ((i == Nx - 1) && (j > Convert.ToInt32(1 / Dy)))
                        {
                            Psi[i, j] = (2 * Psi[i - 1, j] + Psi[i, j - 1] + Psi[i, j + 1]) / 4;
                            continue;
                        }
                        if ((j <= Convert.ToInt32(1 / Dy)) && (i >= Convert.ToInt32((L - 1) / Dx)))
                        {
                            double a = (Nx - i - 1) * Dx - Math.Sqrt(1 - (j * Dy) * (j * Dy));
                            double b = j * Dy - Math.Sqrt(1 - ((Nx - i - 1) * Dx) * ((Nx - i - 1) * Dx));
                            double Psi1 = 0;
                            double Psi2 = 0;
                            if ((a <= 0) || (b <= 0))
                            {
                                Psi[i, j] = 0;
                                continue;
                            }
                            if (a > Dx)
                            {
                                a = Dx;
                                Psi1 = Psi[i + 1, j];
                            }
                            if (b > Dy)
                            {
                                b = Dy;
                                Psi2 = Psi[i, j - 1];
                            }
                            Psi[i, j] = (Psi[i - 1, j] / (Dx * (a + Dx)) + Psi[i, j + 1] / (Dy * (b + Dy)) + Psi1 / (a * (a + Dx)) + Psi2 / (b * (b + Dy))) / (1 / (a * Dx) + 1 / (b * Dy));
                            continue;
                        }
                        Psi[i, j] = (Psi[i - 1, j] + Psi[i + 1, j] + Psi[i, j - 1] + Psi[i, j + 1]) / 4;
                    }
                double norm = 0;
                for (int i = 0; i < Nx; ++i)
                    for (int j = 0; j < Ny; ++j)
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
