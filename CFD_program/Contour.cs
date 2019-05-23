using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFD_program
{
    class Contour
    {
        public double[,] Nodedata { set; get; }
        public double DeltaX { get; set; }
        public double DeltaY { get; set; }
        public double PlotSizeX { get; set; }
        public double PlotSizeY { get; set; }
        public double NumOfLine { get; set; }
        public Graphics G { set; get; }
        private double DataSizeX;
        private double DataSizeY;
        private double ScaleX;
        private double ScaleY;
        private double Datamin;
        private double Datamax;
        public Contour(double[,] Nodedata, double DeltaX, double DeltaY, double PlotSizeX, double PlotSizeY, Graphics G, int NumOfLine = 10)
        {
            //实际坐标为x→ y↑
            //Nodedata的坐标都是x↓ y→
            //PlotSizeX/Y 按照Nodedata的方向
            //GDI+的坐标为x→ y↓
            //画图时上下反向
            this.Nodedata = Nodedata;
            this.DeltaX = DeltaX;
            this.DeltaY = DeltaY;
            this.PlotSizeX = PlotSizeX;
            this.PlotSizeY = PlotSizeY;
            this.G = G;
            this.NumOfLine = NumOfLine;
            DataSizeX = (Nodedata.GetLength(0) - 1) * DeltaX;
            DataSizeY = (Nodedata.GetLength(1) - 1) * DeltaY;
            ScaleX = PlotSizeX / DataSizeX;
            ScaleY = PlotSizeY / DataSizeY;
            Datamin = Nodedata[0, 0];
            Datamax = Nodedata[0, 0];
            foreach (double ele in Nodedata)
            {
                if (ele > Datamax) Datamax = ele;
                if (ele < Datamin) Datamin = ele;
            }
        }
        public void DrawContourLines()
        {
            double CurrentValue = Datamin;
            double DeltaValue = (Datamax - Datamin) / (NumOfLine + 1);
            double DeltaG = 255 / (NumOfLine - 1);
            for (int k = 0; k < NumOfLine; ++k)
            {
                Pen pen = new Pen(Color.FromArgb(255, Convert.ToInt32(k * DeltaG), 0));
                CurrentValue += DeltaValue;
                for (int i = 0; i < Nodedata.GetLength(0) - 1; ++i)//节点左上角标号代表一个网格
                    for (int j = 0; j < Nodedata.GetLength(1) - 1; ++j)
                    {
                        //判断四个角点与CurrentValue的大小关系
                        int UpperLeft = Convert.ToInt32(Nodedata[i, j] > CurrentValue);
                        int UpperRight = Convert.ToInt32(Nodedata[i, j + 1] > CurrentValue);
                        int LowerLeft = Convert.ToInt32(Nodedata[i + 1, j] > CurrentValue);
                        int LowerRight = Convert.ToInt32(Nodedata[i + 1, j + 1] > CurrentValue);
                        int Cases = UpperLeft * 8 + LowerLeft * 4 + LowerRight * 2 + UpperRight;
                        //线性插值
                        float UpperX = Convert.ToSingle(i * DeltaX * ScaleX);
                        float UpperY = Convert.ToSingle(((CurrentValue - Nodedata[i, j]) / (Nodedata[i, j + 1] - Nodedata[i, j]) + j) * DeltaY * ScaleY);
                        PointF Upper = new PointF(UpperX, UpperY);
                        float LeftX = Convert.ToSingle(((CurrentValue - Nodedata[i, j]) / (Nodedata[i + 1, j] - Nodedata[i, j]) + i) * DeltaX * ScaleX);
                        float LeftY = Convert.ToSingle(j * DeltaY * ScaleY);
                        PointF Left = new PointF(LeftX, LeftY);
                        float RightX = Convert.ToSingle(((CurrentValue - Nodedata[i, j + 1]) / (Nodedata[i + 1, j + 1] - Nodedata[i, j + 1]) + i) * DeltaX * ScaleX);
                        float RightY = Convert.ToSingle((j + 1) * DeltaY * ScaleY);
                        PointF Right = new PointF(RightX, RightY);
                        float LowerX = Convert.ToSingle((i + 1) * DeltaX * ScaleX);
                        float LowerY = Convert.ToSingle(((CurrentValue - Nodedata[i + 1, j]) / (Nodedata[i + 1, j + 1] - Nodedata[i + 1, j]) + j) * DeltaY * ScaleY);
                        PointF Lower = new PointF(LowerX, LowerY);
                        
                        switch (Cases)
                        {
                            case 0://0000
                                break;
                            case 1://0001
                                G.DrawLine(pen, Upper, Right);
                                break;
                            case 2://0010
                                G.DrawLine(pen, Lower, Right);
                                break;
                            case 3://0011
                                G.DrawLine(pen, Lower, Upper);
                                break;
                            case 4://0100
                                G.DrawLine(pen, Lower, Left);
                                break;
                            case 5://0101
                                G.DrawLine(pen, Left, Upper);
                                G.DrawLine(pen, Right, Lower);
                                break;
                            case 6://0110
                                G.DrawLine(pen, Left, Right);
                                break;
                            case 7://0111
                                G.DrawLine(pen, Left, Upper);
                                break;
                            case 8://1000
                                G.DrawLine(pen, Left, Upper);
                                break;
                            case 9://1001
                                G.DrawLine(pen, Left, Right);
                                break;
                            case 10://1010
                                G.DrawLine(pen, Left, Lower);
                                G.DrawLine(pen, Right, Upper);
                                break;
                            case 11://1011
                                G.DrawLine(pen, Lower, Left);
                                break;
                            case 12://1100
                                G.DrawLine(pen, Lower, Upper);
                                break;
                            case 13://1101
                                G.DrawLine(pen, Lower, Right);
                                break;
                            case 14://1110
                                G.DrawLine(pen, Upper, Right);
                                break;
                            case 15://1111
                                break;
                            default:
                                break;
                        }
                    }
            }
        }
    }
}
