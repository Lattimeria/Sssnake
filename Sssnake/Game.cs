using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;
using System.IO;

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
        public delegate string AskNameDelegate();

        public AskNameDelegate AskName;

        public Action DoStageUp;

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
                    //StageUp = true;
                    DoStageUp();
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
        public void Draw(System.Drawing.Graphics graphics, int S, List<Scores> ListScores) //отрисовка
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
                Scores scores = new Scores();

                string path = Application.StartupPath + "\\records.txt";
                ListScores = ReadAndDeserialize(path);
                               

                scores.Name = AskName?.Invoke() ?? ""; // сделать ввод имени
                scores.Level = stage;
                scores.EatingFood = foods;
                scores.Score = score;
                ListScores.Add(scores);

                SerializeAndSave(path, ListScores);
                
                DrawGameOver(graphics, S, ListScores);
            }
        }
        public List<Scores> ReadAndDeserialize(string path)
        {
            if (!File.Exists(path))
                return new List<Scores>();

            var serializer = new XmlSerializer(typeof(List<Scores>));
            using (var reader = new StreamReader(path))
            {
                return (List<Scores>)serializer.Deserialize(reader);
            }
        }

        public void SerializeAndSave(string path, List<Scores> data)
        {
            var serializer = new XmlSerializer(typeof(List<Scores>));
            using (var writer = new StreamWriter(File.Open(path, FileMode.OpenOrCreate)))
            {
                serializer.Serialize(writer, data);
            }
        }

        public void DrawGameOver(System.Drawing.Graphics graphics, int S, List<Scores> ListScores) //отрисовка
        {
            int count = ListScores.Count()-1;
            var style = System.Drawing.FontStyle.Regular;
            var color = System.Drawing.Brushes.Black;

            string str = "For a new game, press any key";
            graphics.DrawString(str, new System.Drawing.Font("Arial", 10, System.Drawing.FontStyle.Bold), color, new System.Drawing.Point(5, 5));

            string stateName = "Name" + "\t" + "Stage" + "\t" + "Food" + "\t" + "Score";
            graphics.DrawString(stateName, new System.Drawing.Font("Arial", 10, System.Drawing.FontStyle.Italic), color, new System.Drawing.Point(5, 20));
            graphics.DrawString("Your record:", new System.Drawing.Font("Arial", 10, style), color, new System.Drawing.Point(60, 35));

            string stateCurrently = ListScores[count].Name + "\t" + ListScores[count].Level + "\t" +
                ListScores[count].EatingFood + "\t" + ListScores[count].Score;
            graphics.DrawString(stateCurrently, new System.Drawing.Font("Arial", 9, style), color, new System.Drawing.Point(5, 50));

            graphics.DrawString("Best records:", new System.Drawing.Font("Arial", 10, style), color, new System.Drawing.Point(60, 65));

            int indent = 80;

            ListScores.Sort();
            foreach (Scores aScores in ListScores)
            {
                string stateBest = aScores.Name + "\t" + aScores.Level + "\t" +
                    aScores.EatingFood + "\t" + aScores.Score;
                graphics.DrawString(stateBest, new System.Drawing.Font("Arial", 9, style), color, new System.Drawing.Point(5, indent));
                indent += 15;
            }
            /*
            for (int i = 0; i <= count; i++)
            {
                string stateBest =  ListScores[i].Name + "\t" + ListScores[i].Level + "\t" +
                    ListScores[i].EatingFood + "\t" + ListScores[i].Score;
                graphics.DrawString(stateBest, new System.Drawing.Font("Arial", 9, style), color, new System.Drawing.Point(5, indent));
                indent += 15;
            }*/
        }

        void SortListScores(List<Scores> ListScores)
        {
            
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

    [Serializable]
    public class Scores: IComparable
    {
        public string Name = null;
        public int Level = 0;
        public int EatingFood = 0;
        public int Score = 0;

        public int CompareTo(object obj)
        {
            if (obj is Scores otherScore)
            {
                if (Score > otherScore.Score)
                    return -1;
                else if (Score < otherScore.Score)
                    return 1;
                else
                    return 0;
            }

            return 0;               
        }
    }
}
