using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Fractals
{
    public class PythagorasTree : Fractal
    {
        public double angle = Math.PI / 2; 
        public double ang1 = Math.PI / 4;  
        public double ang2 = Math.PI / 6; 
        public PythagorasTree(int itarationCount, Graphics gr, Color drawColor, Color backgroundColor, int viewBoxSize)
            : base(itarationCount, gr, drawColor, backgroundColor, viewBoxSize)
        {

        }

        public override void Draw()
        {
            Gr.Clear(Color.White);
            DrawTree(ViewBoxSize / 2, 3 * ViewBoxSize / 4, ViewBoxSize / 3, angle, ItarationCount);
        }

        public void DrawTree(double x, double y, double a, double angle, int n)
        {
            if (n > 0)
            {
                a *= 0.7; 

                //Считаем координаты для вершины-ребенка.
                double xnew = Math.Round(x + a * Math.Cos(angle)),
                       ynew = Math.Round(y - a * Math.Sin(angle));

                //рисуем линию между вершинами.
                Gr.DrawLine(DrawPen, (float)x, (float)y, (float)xnew, (float)ynew);

                //Переприсваеваем координаты.
                x = xnew;
                y = ynew;

                //Вызываем рекурсивную функцию для левого и правого ребенка.
                DrawTree(x, y, a, angle + ang1, n-1);
                DrawTree(x, y, a, angle - ang2, n-1);
            }
        }
    }
}
