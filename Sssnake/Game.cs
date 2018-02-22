using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sssnake
{
    enum Heading //движение змеи, 0 - вверх, 1 - вправо, 2 - вниз, 3 - влево
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
        int foods = 0; //кол-во съеденной еды
        Random rand = new Random();
        public int score = 0; //рекорд
        int stage = 1; //уровень (за каждые 10 еды +1 уровень)
        public bool gameFinished = false;

        public bool StageUp = false;

        public void Start(int W, int H, int S)
        {
            int headX = W / 2, headY = H / 2; //начальные координаты змеи
            snake = new Snake(headX, headY);

            food = new Food();
            AddFood(W, H);

        }
        void AddFood(int W, int H) // добавление еды на поле
        {
            do
            {
                food.foodCoordinate.X = rand.Next(W);
                food.foodCoordinate.Y = rand.Next(H);
            }
            while (snake.CheckCollision(food.foodCoordinate.X, food.foodCoordinate.Y) == true);

        }

        public void Update(int W, int H) //проверка столкновений
        {

            snake.Update(W, H);
            if (snake.EatHimself() == true)
            {
                gameFinished = true;
            }
            if (snake.segments[0].X == food.foodCoordinate.X && snake.segments[0].Y == food.foodCoordinate.Y)
            {
                AddFood(W, H);
                foods++;
                score += stage;
                if (foods % 10 == 0)
                {
                    stage++;
                    StageUp = true;
                }
                snake.Grow();
            }
        }
        public void KeyDown(int pressKey, int W, int H)
        {

            switch (pressKey)
            {
                case 0:
                    if (snake.CurrentHeading != Heading.Bottom)
                        snake.FutureHeading = Heading.Top;
                    break;
                case 2:
                    if (snake.CurrentHeading != Heading.Top)
                        snake.FutureHeading = Heading.Bottom;
                    break;
                case 1:
                    if (snake.CurrentHeading != Heading.Left)
                        snake.FutureHeading = Heading.Right;
                    break;
                case 3:
                    if (snake.CurrentHeading != Heading.Right)
                        snake.FutureHeading = Heading.Left;
                    break;
            }
        }
        public void Draw(System.Drawing.Graphics graphics, int S, List<int> ListScores) //отрисовка
        {
            if (!gameFinished)
            {
                food.Draw(graphics, S);
                snake.Draw(graphics, S);

                string state = "Score:" + score.ToString();
                graphics.DrawString(state, new System.Drawing.Font("Arial", 10, System.Drawing.FontStyle.Italic), System.Drawing.Brushes.Black, new System.Drawing.Point(5, 5));
            }
            else
            {
                ListScores.Add(score);
                int count = ListScores.Count - 1;
                for (int i = count-1; i > 0; i--)
                {
                    if (ListScores[count] > ListScores[i])
                    {
                        int temp = ListScores[i];
                        ListScores[i] = ListScores[count];
                        ListScores[count] = temp;
                    }
                        
                }

                DrawGameOver(graphics, S, ListScores);
            }
        }
        public void DrawGameOver(System.Drawing.Graphics graphics, int S, List<int> ListScores) //отрисовка
        {

            string state = "Your record:" + "\n" + "Stage:" + stage.ToString() + "\n" +
                "Food:" + foods.ToString() + "\n" + "Score:" + score.ToString() + "\n" +
                "For a new game, press any key" + "\n";

            graphics.DrawString(state, new System.Drawing.Font("Arial", 10, System.Drawing.FontStyle.Italic), System.Drawing.Brushes.Black, new System.Drawing.Point(5, 5));

            graphics.DrawString("Best records:" + "\n", new System.Drawing.Font("Arial", 10, System.Drawing.FontStyle.Italic), System.Drawing.Brushes.Black, new System.Drawing.Point(5, 80));
            for (int i = 0; i < ListScores.Count; i++)
            {
                graphics.DrawString(ListScores[i].ToString() + "\n", new System.Drawing.Font("Arial", 10, System.Drawing.FontStyle.Italic), System.Drawing.Brushes.Black, new System.Drawing.Point(5, 95));
            }
        }
    }
    public struct Coordinate
    {
        public int X;
        public int Y;
        public Coordinate(int x, int y)
        {
            X = x;
            Y = y;
        }

        public static bool operator ==(Coordinate left, Coordinate right)
        {
            return left.X == right.X && left.Y == right.Y;
        }

        public static bool operator !=(Coordinate left, Coordinate right)
        {
            return left.X != right.X || left.Y != right.Y;
        }
    }

}
