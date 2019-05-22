using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlowAroundCylinder
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
        public Contour(double[,] Nodedata, double DeltaX, double DeltaY, double PlotSizeX, double PlotSizeY, Graphics G, double NumOfLine = 10)
        {
            //实际坐标为x→ y↑
            //Nodedata的坐标都是x↓ y→
            //PlotSizeX/Y 按照Nodedata的方向
            //画图时反着画
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
            for (int k = 0; k < NumOfLine; ++k)
            {
                CurrentValue += DeltaValue;
                for (int i = 0; i < Nodedata.GetLength(0) - 1; ++i)//节点左上角标号代表一个网格
                    for (int j = 0; j < Nodedata.GetLength(1) - 1; ++j)
                    {
                        int UpperLeft = Convert.ToInt32(Nodedata[i, j] > CurrentValue);
                        int UpperRight = Convert.ToInt32(Nodedata[i, j + 1] > CurrentValue);
                        int LowerLeft = Convert.ToInt32(Nodedata[i + 1, j] > CurrentValue);
                        int LowerRight = Convert.ToInt32(Nodedata[i + 1, j + 1] > CurrentValue);
                        int Cases = UpperLeft << 3 + LowerLeft << 2 + LowerRight << 1 + UpperRight;

                        float UpperX = Convert.ToSingle(i * DeltaX * ScaleX);
                        float UpperY = Convert.ToSingle(((CurrentValue - Nodedata[i, j]) / (Nodedata[i, j + 1] - Nodedata[i, j]) + j) * DeltaY * ScaleY);

                        float LeftX = Convert.ToSingle(((CurrentValue - Nodedata[i, j]) / (Nodedata[i + 1, j] - Nodedata[i, j]) + i) * DeltaX * ScaleX);
                        float LeftY = Convert.ToSingle(j * DeltaY * ScaleY);

                        float RightX = Convert.ToSingle(((CurrentValue - Nodedata[i, j + 1]) / (Nodedata[i + 1, j + 1] - Nodedata[i, j + 1]) + i) * DeltaX * ScaleX);
                        float RightY = Convert.ToSingle((j + 1) * DeltaY * ScaleY);

                        float LowerX = Convert.ToSingle((i + 1) * DeltaX * ScaleX);
                        float LowerY = Convert.ToSingle(((CurrentValue - Nodedata[i + 1, j]) / (Nodedata[i + 1, j + 1] - Nodedata[i + 1, j]) + j) * DeltaY * ScaleY);


                        //画图时反向XY坐标
                        switch (Cases)
                        {
                            case 0://0000
                                break;
                            case 1://0001
                                G.DrawLine(Pens.Black, UpperY, UpperX, RightY, RightX);//右上
                                break;
                            case 2://0010
                                G.DrawLine(Pens.Black, LowerY, LowerX, RightY, RightX);//右下
                                break;
                            case 3://0011
                                G.DrawLine(Pens.Black, LowerY, LowerX, UpperY, UpperX);//上下
                                break;
                            case 4://0100
                                G.DrawLine(Pens.Black, LowerY, LowerX, LeftY, LeftX);//左下
                                break;
                            case 5://0101
                                
                                break;
                            case 6:
                                break;
                            case 7:
                                break;
                            case 8:
                                break;
                            case 9:
                                break;
                            case 10:
                                break;
                            case 11:
                                break;
                            case 12:
                                break;
                            case 13:
                                break;
                            case 14:
                                break;
                            case 15:
                                break;
                        }





                    }
            }
        }
    }

}
