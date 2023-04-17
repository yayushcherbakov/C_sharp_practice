using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Fractals
{
    public class KochCurve: Fractal
    {
        public KochCurve(int itarationCount, Graphics gr, Color drawColor, Color backgroundColor, int viewBoxSize) 
            :base(itarationCount, gr, drawColor, backgroundColor, viewBoxSize)
        {

        }

        public override void Draw()
        {
            Gr.Clear(Color.White);
            float w = ViewBoxSize;
            float h = ViewBoxSize;
            DrawStep(new PointF(w / 2, 7 * h / 5),
                new PointF((float)(w / 2 - 3 * w / (5 * Math.Sqrt(3))), 4 * h / 5),
                new PointF((float)(w / 2 + 3 * w / (5 * Math.Sqrt(3))), 4 * h / 5),
                ItarationCount);
        }

        /// <summary>
        /// Отрисовка шага
        /// </summary>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <param name="p3"></param>
        /// <param name="n"></param>
        /// <returns></returns>
        private int DrawStep(PointF p1, PointF p2, PointF p3, int n)
        {
            if (n == ItarationCount)
            {
                Gr.DrawLine(DrawPen, p2, p3);

                DrawStep(p2, p3, p1, n - 1);
            }
            else if (n > 0)
            {
                var p4 = new PointF((p2.X + 2 * p1.X) / 3, (p2.Y + 2 * p1.Y) / 3);
                var p5 = new PointF((2 * p2.X + p1.X) / 3, (p1.Y + 2 * p2.Y) / 3);
                var ps = new PointF((p2.X + p1.X) / 2, (p2.Y + p1.Y) / 2);
                var pn = new PointF((4 * ps.X - p3.X) / 3, (4 * ps.Y - p3.Y) / 3);

                Gr.DrawLine(DrawPen, p4, pn);
                Gr.DrawLine(DrawPen, p5, pn);
                Gr.DrawLine(BackgroundPen, p4, p5);

                DrawStep(p4, pn, p5, n - 1);
                DrawStep(pn, p5, p4, n - 1);
                DrawStep(p1, p4, new PointF((2 * p1.X + p3.X) / 3,
                    (2 * p1.Y + p3.Y) / 3), n - 1);
                DrawStep(p5, p2, new PointF((2 * p2.X + p3.X) / 3,
                    (2 * p2.Y + p3.Y) / 3), n - 1);

            }
            return n;
        }
    }
}
