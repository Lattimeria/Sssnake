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
        public void Draw(System.Drawing.Graphics graphics, int W, int H)
        {
            graphics.FillRectangle(System.Drawing.Brushes.Green, this.foodCoordinate.X, this.foodCoordinate.Y, W, H);
        }
    }
    
}
