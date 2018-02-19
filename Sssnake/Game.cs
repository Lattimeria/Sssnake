using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sssnake
{
    class Game
    {
        public int W = 40, H = 30, S = 10; //ширина и высота поля и размер ячейки
        Snake snake;
        Food food;
        int foods; //кол-во съеденной еды
        Random rand = new Random();
        
        public void Start()
        {
            int headX = W, headY = H;
            snake = new Snake(headX,headY);
            

            food = new Food();
            food.foodCoordinate.X = rand.Next(W);
            food.foodCoordinate.Y = rand.Next(H);
            
        } 
        void Update() //проверка столкновений
        {

        }
        public void Draw(System.Drawing.Graphics graphics)
        {
            food.Draw(graphics,S,S);
            snake.Draw(graphics,S,S);
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
