using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Fractals
{
    public class CantorSet : Fractal
    {
        public CantorSet(int itarationCount, Graphics gr, Color drawColor, Color backgroundColor, int viewBoxSize)
            : base(itarationCount, gr, drawColor, backgroundColor, viewBoxSize)
        {

        }

        public override void Draw()
        {
            Gr.Clear(Color.White);

            var width = 5 * ViewBoxSize / 7;
            int x = ViewBoxSize / 2 - width / 2;
            DrawSet(x, 10, width, ItarationCount);
        }

        private void DrawSet(int x, int y, int width, int n)
        {
            //заливка 
            SolidBrush Black = new SolidBrush(Color.Black);
            Pen myPen = new Pen(Color.Black, 1);

            //Поставим условие вызова и прорисовки 

            if (n > 0)
            {
                //Отрезки изображаем прямоугольниками для наглядности
                Gr.DrawRectangle(myPen, x, y, width, 12);
                Gr.FillRectangle(Black, x, y, width, 12);
                //Сдвигаемся вниз
                y = y + 40;
                //Вызываем функцию для двух полученных отрезков
                DrawSet(x + width * 2 / 3, y, width / 3, n - 1);
                DrawSet(x, y, width / 3, n - 1);
            }
        }
    }
}
