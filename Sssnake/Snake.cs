using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sssnake
{
    class Snake
    {
        public List<Coordinate> segments;
        public int way = 0;
        public Snake(int headX,int headY) //задание начальных ячеек змеи
        {
            segments = new List<Coordinate>();
            segments.Add(new Coordinate(headX, headY-3));
            segments.Add(new Coordinate(headX, headY-2));
            segments.Add(new Coordinate(headX, headY-1));
        }
        public void Update(int W,int H) //двигаем змею
        {
            var headX = segments[0].X;
            var headY = segments[0].Y;
            switch (way)
            {
                case 0:
                    headY--;
                    if (headY < 0)
                        headY = H - 1;
                    break;
                case 1:
                    headX++;
                    if (headX >= W)
                        headX = 0;
                    break;
                case 2:
                    headY++;
                    if (headY >= H)
                        headY = 0;
                    break;
                case 3:
                    headX--;
                    if (headX < 0)
                        headX = W - 1;
                    break;
            }
            Coordinate newHead = new Coordinate(headX, headY);
            segments.Insert(0, newHead);
        }

        public void Draw(System.Drawing.Graphics graphics, int S) //отрисовка змеи
        {
            /*foreach(var segment in segments)
            {
                graphics.FillRectangle(System.Drawing.Brushes.Red, segment.X*S, segment.Y*S, S, S);
            }*/
            graphics.FillRectangle(System.Drawing.Brushes.Red, new System.Drawing.Rectangle(segments[0].X * S, segments[0].Y * S, S, S));
            for (int i = 1; i < segments.Count; i++)
                graphics.FillRectangle(System.Drawing.Brushes.Pink, new System.Drawing.Rectangle(segments[i].X * S, segments[i].Y * S, S, S));
        }
    }
}
