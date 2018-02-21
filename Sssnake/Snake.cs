using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sssnake
{
    class Snake
    {
        public int Length;
        public List<Coordinate> segments;
        
        public Heading FutureHeading;
        public Heading CurrentHeading { get; private set; } = 0;

        public Snake(int headX, int headY) //задание начальных ячеек змеи
        {
            segments = new List<Coordinate>();
            segments.Add(new Coordinate(headX, headY - 3));
            segments.Add(new Coordinate(headX, headY - 2));
            segments.Add(new Coordinate(headX, headY - 1));
            Length = segments.Count;
        }
        public void Update(int W, int H) //двигаем змею
        {
            CurrentHeading = FutureHeading;
            var headX = segments[0].X;
            var headY = segments[0].Y;
            switch (CurrentHeading)
            {
                case Heading.Top:
                    headY--;
                    if (headY < 0)
                        headY = H;
                    break;
                case Heading.Right:
                    headX++;
                    if (headX > W)
                        headX = 0;
                    break;
                case Heading.Bottom:
                    headY++;
                    if (headY > H)
                        headY = 0;
                    break;
                case Heading.Left:
                    headX--;
                    if (headX < 0)
                        headX = W;
                    break;
            }
            Coordinate newHead = new Coordinate(headX, headY);

            for (int i = segments.Count - 1; i > 0; i--)
            {
                segments[i] = segments[i - 1];
            }

            while (Length > segments.Count)
            {
                segments.Add(segments[segments.Count - 1]);
            }

            segments[0] = newHead;
        }
        public void Grow()//ростим змею
        {
            Length++;
        } 
        public bool CheckCollision(int coordX, int coordY) //проверка на столкновения змеи и еды
        {
            for (int i = 0; i < segments.Count; i++)
            {
                if (coordX == segments[i].X && coordY == segments[i].Y)
                    return true;
            }
            return false;
        }
        public bool EatHimself() // проверка на столкновение змеи с собой
        {
            for (int i = 1; i < segments.Count; i++)
            {
                if (segments[0].X == segments[i].X && segments[0].Y == segments[i].Y)
                    return true;
            }
            return false;
        }
        public void Draw(System.Drawing.Graphics graphics, int S) //отрисовка змеи
        {
            graphics.FillRectangle(System.Drawing.Brushes.Red, new System.Drawing.Rectangle(segments[0].X * S, segments[0].Y * S, S, S));
            for (int i = 1; i < segments.Count; i++)
                graphics.FillRectangle(System.Drawing.Brushes.Pink, new System.Drawing.Rectangle(segments[i].X * S, segments[i].Y * S, S, S));
        }
    }
}
