using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Fractals
{
    public class SerpinskiTriangle : Fractal
    {
        public SerpinskiTriangle(int itarationCount, Graphics gr, Color drawColor, Color backgroundColor, int viewBoxSize)
            : base(itarationCount, gr, drawColor, backgroundColor, viewBoxSize)
        {

        }

        public override void Draw()
        {
            Gr.Clear(Color.White);
            PointF topPoint = new PointF(ViewBoxSize / 2f, 0);
            PointF leftPoint = new PointF(0, ViewBoxSize);
            PointF rightPoint = new PointF(ViewBoxSize, ViewBoxSize);
            //вызываем функцию отрисовки.
            DrawTriangle(ItarationCount, topPoint, leftPoint, rightPoint);
        }

        private void DrawTriangle(int level, PointF top, PointF left, PointF right)
        {
            //проверяем, закончили ли мы построение.
            if (level == 0)
            {
                PointF[] points = new PointF[3]
                {
                    top, right, left
                };
                //рисуем фиолетовый треугольник.
                Gr.FillPolygon(Brushes.BlueViolet, points);
            }
            else
            {
                //вычисляем среднюю точку.
                var leftMid = MidPoint(top, left);
                var rightMid = MidPoint(top, right); 
                var topMid = MidPoint(left, right); 
                //рекурсивно вызываем функцию для каждого и 3 треугольников.
                DrawTriangle(level - 1, top, leftMid, rightMid);
                DrawTriangle(level - 1, leftMid, left, topMid);
                DrawTriangle(level - 1, rightMid, topMid, right);
            }
        }

        private PointF MidPoint(PointF p1, PointF p2)
        {
            return new PointF((p1.X + p2.X) / 2f, (p1.Y + p2.Y) / 2f);
        }
    }
}
