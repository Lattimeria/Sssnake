using System;
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
        Game game=new Game();
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

            int caption_size = SystemInformation.CaptionHeight; // высота шапки формы
            int frame_size = SystemInformation.FrameBorderSize.Height; // ширина границы формы
            // устанавливаем размер внутренней области формы W * H с учетом высоты шапки и ширины границ
            this.Size = new Size(game.W * game.S, game.H * game.S);

            this.Paint += new PaintEventHandler(Program_Paint); // обработчик прорисовки формы
            this.KeyDown += new KeyEventHandler(Program_KeyDown); // обработчик нажатий на кнопки
        }
        
        void Program_KeyDown(object sender, KeyEventArgs e)
        {

        }

        void Program_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            //Pen pen = new Pen(Color.Red);
            game.Draw(g);
        }
        
        public void StartGame()
        {
            //game = new Game();
            game.Start();
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            //Game.Draw(Graphics);
        }
    }
}
