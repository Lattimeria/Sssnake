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
        public Form1()
        {
            InitializeComponent();

            this.FormBorderStyle = FormBorderStyle.FixedDialog; // мышкой нельзя растягивать форму
            this.MaximizeBox = false; // недоступна кнопка "развернуть во весь экран"
            this.StartPosition = FormStartPosition.CenterScreen; // форма по центру экрана

            Game game = new Game();
            
        }
        // классы: игра, змея, еда
        // 
        // еда появляется на поле, может быть съедена змеей, после исчезает
        // змея двигается по полю
        // игра следит за координатами, создает еду, отслеживает пересечения и решает что делать
        // добавить длину змеи или закончить игру

        /*Pen myPen = new Pen(Color.Red);
        Graphics g;
        g = this.CreateGraphics();
        g.DrawRectangle(myPen, new Rectangle(0, 0, 30, 30));
            myPen.Dispose();
            g.Dispose();
            */
        
            


    }
}
