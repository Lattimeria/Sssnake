using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sssnake
{
    class Food
    {
        public Coordinate foodCoordinate;
        public void Draw(System.Drawing.Graphics graphics, int S)
        {
            graphics.FillRectangle(System.Drawing.Brushes.Green, this.foodCoordinate.X * S, this.foodCoordinate.Y * S, S, S);
        }
    }

}
