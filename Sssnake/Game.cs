using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sssnake
{
    enum Heading
    {
        Top,
        Right,
        Bottom,
        Left
    }
    class Game
    {
        Snake snake;
        Food food;
        int foods=0; //кол-во съеденной еды
        Random rand = new Random();
        
        public void Start(int W, int H, int S)
        {
            int headX = W/2, headY = H/2;
            snake = new Snake(headX,headY);
            int way = snake.way;

            food = new Food();
            food.foodCoordinate.X = rand.Next(W);
            food.foodCoordinate.Y = rand.Next(H);
            
        } 
        public void Update(int W, int H) //проверка столкновений
        {
            snake.Update(W,H);

            if (snake.segments[0].X == food.foodCoordinate.X && snake.segments[0].Y == food.foodCoordinate.Y)
            {
                food.foodCoordinate.X = rand.Next(W);
                food.foodCoordinate.Y = rand.Next(H);
                foods++;

                // положить в метод в змейке (Grow)
                snake.Length++;
            }
        }
        public void KeyDown(int pressKey,int W,int H)
        {
            switch (pressKey)
            {
                case 0:
                    if (snake.way != 2)
                        snake.way = 0;
                    break;
                case 2:
                    if (snake.way != 0)
                        snake.way = 2;
                    break;
                case 1:
                    if (snake.way != 3)
                        snake.way = 1;
                    break;
                case 3:
                    if (snake.way != 1)
                        snake.way = 3;
                    break;
            }

           
        }
        public void Draw(System.Drawing.Graphics graphics, int S)
        {
            food.Draw(graphics,S);
            snake.Draw(graphics,S);
        }
        
    }
    public struct  Coordinate
    {
        public int X;
        public int Y;
        public Coordinate(int x,int y)
        {
            X = x;
            Y = y;
        }
    }
}
