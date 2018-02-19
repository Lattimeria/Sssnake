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
        public Snake(int headX,int headY)
        {
            segments = new List<Coordinate>();
            segments.Add(new Coordinate(headX, headY));
            segments.Add(new Coordinate(headX+10, headY));
            segments.Add(new Coordinate(headX+20, headY));
        }
        public void Update() //двигаем змею
        {

        }

        public void Draw(System.Drawing.Graphics graphics,  int W, int H) //отрисовка змеи
        {
            foreach(var segment in segments)
            {
                graphics.FillRectangle(System.Drawing.Brushes.Red, segment.X, segment.Y, W, H);
            }
            
        }
    }
}
