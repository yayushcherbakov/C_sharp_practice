using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Fractals
{
    public class SerpinskiCarpet : Fractal
    {
        public SerpinskiCarpet(int itarationCount, Graphics gr, Color drawColor, Color backgroundColor, int viewBoxSize)
            : base(itarationCount, gr, drawColor, backgroundColor, viewBoxSize)
        {

        }

        public override void Draw()
        {
            Gr.Clear(Color.White);
            RectangleF carpet = new RectangleF(0, 0, ViewBoxSize, ViewBoxSize);
            DrawCarpet(ItarationCount, carpet);
        }

        private void DrawCarpet(int level, RectangleF carpet)
        {
            //проверяем, закончили ли мы построение.
            if (level == 0)
            {
                //Рисуем прямоугольни.
                Gr.FillRectangle(Brushes.OrangeRed, carpet);
            }
            else
            {
                // делим прямоугольник на 9 частей.
                var width = carpet.Width / 3f;
                var height = carpet.Height / 3f;
                // (x1, y1) - координаты левой верхней вершины прямоугольника.
                // от нее будем отсчитывать остальные вершины маленьких прямоугольников.
                var x1 = carpet.Left;
                var x2 = x1 + width;
                var x3 = x1 + 2f * width;

                var y1 = carpet.Top;
                var y2 = y1 + height;
                var y3 = y1 + 2f * height;

                // левый 1(верхний).
                DrawCarpet(level - 1, new RectangleF(x1, y1, width, height));
                // средний 1.
                DrawCarpet(level - 1, new RectangleF(x2, y1, width, height));
                // правый 1.
                DrawCarpet(level - 1, new RectangleF(x3, y1, width, height));
                // левый 2.
                DrawCarpet(level - 1, new RectangleF(x1, y2, width, height));
                // правый 2.
                DrawCarpet(level - 1, new RectangleF(x3, y2, width, height));
                // левый 3.
                DrawCarpet(level - 1, new RectangleF(x1, y3, width, height));
                // средний 3.
                DrawCarpet(level - 1, new RectangleF(x2, y3, width, height));
                // правый 3.
                DrawCarpet(level - 1, new RectangleF(x3, y3, width, height)); 
            }
        }
    }
}
