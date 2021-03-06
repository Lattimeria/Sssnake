﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sssnake
{
    public partial class Form1 : Form
    {
        public int W = 30, H = 30, S = 10; //ширина и высота поля и размер ячейки
        public List<Scores> ListRecords = new List<Scores>();
        Game game;
        public Form1()
        {
            InitializeComponent();
            InitializeForm();

            StartGame();

        }
        void InitializeForm()
        {
            this.FormBorderStyle = FormBorderStyle.FixedDialog; // мышкой нельзя растягивать форму
            this.MaximizeBox = false; // недоступна кнопка "развернуть во весь экран"
            this.StartPosition = FormStartPosition.CenterScreen; // форма по центру экрана
            this.DoubleBuffered = true;

            this.Paint += new PaintEventHandler(Program_Paint); // обработчик прорисовки формы
            this.KeyDown += new KeyEventHandler(Program_KeyDown); // обработчик нажатий на кнопки

            this.ClientSize = new Size((W + 1) * S, (H + 1) * S);
        }

        void Program_KeyDown(object sender, KeyEventArgs e) //обработчик нажатия клавиш
        {
            int pressKey = 0;
            if (!game.gameFinished)
            {
                switch (e.KeyData)
                {
                    case Keys.Up:
                        pressKey = 0;
                        break;
                    case Keys.Down:
                        pressKey = 2;
                        break;
                    case Keys.Right:
                        pressKey = 1;
                        break;
                    case Keys.Left:
                        pressKey = 3;
                        break;
                }
                game.KeyDown(pressKey, W, H);
                UpdateAndDraw();
            }
            else
            {
                StartGame();
            }
        }

        void Program_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.FillRectangle(Brushes.White, 0, 0, this.Width, this.Height);
            game.Draw(g, S, ListRecords);
            if (game.gameFinished)
                timer1.Stop();
        }

        public void StartGame()
        {
            
            game = new Game();
            game.AskName += AskName;
            game.DoStageUp += () =>  timer1.Interval -= 10; 
            game.Start(W, H, S);
            //timer1 = new Timer();
            timer1.Interval = 200;
            timer1.Tick += new EventHandler(timer1_Tick);
            timer1.Start();
        }
        
        public string AskName()
        {
            return "Test";
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            UpdateAndDraw();
            if(game.StageUp == true)
            {
                timer1.Interval -= 10;
                game.StageUp = false;
            }
        }

        void UpdateAndDraw()
        {
            game.Update(W, H);
            Refresh();
        }
    }
}
