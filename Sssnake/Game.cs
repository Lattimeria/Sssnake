using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sssnake
{
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
        }
        public void KeyDown(int pressKey,int W,int H)
        {
            int snakeWay = snake.way;
            switch (pressKey)
            {
                case 0:
                    if (snakeWay != 2)
                        snakeWay = 0;
                    break;
                case 2:
                    if (snakeWay != 0)
                        snakeWay = 2;
                    break;
                case 1:
                    if (snakeWay != 3)
                        snakeWay = 1;
                    break;
                case 3:
                    if (snakeWay != 1)
                        snakeWay = 3;
                    break;
            }
            if(snake.segments[0].X==food.foodCoordinate.X && snake.segments[0].Y == food.foodCoordinate.Y)
            {
                food.foodCoordinate.X = rand.Next(W);
                food.foodCoordinate.Y = rand.Next(H);
                foods++;
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
