using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Kefir_BattleCity
{
    public partial class Form1 : Form
    {
        LoadData loaddata = new LoadData();

        Game game = new Game();
        public Form1()
        {
            MessageBox.Show("Нажмите ОК для начала новой игры");
            InitializeComponent();

        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            timer1.Interval = 70;
            game.InitGame(loaddata);
        }
        Color GetTileColor(TileType type)
        {
            switch (type)
            {
                case TileType.Concrete:
                    return Color.Gray;
                case TileType.Brick:
                    return Color.Brown;
                case TileType.Tower:
                    return Color.Red;
                case TileType.Water:
                    return Color.Blue;
                case TileType.Empty:
                default:
                    return Color.Black;
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Up:
                    game.tnk.MoveUp();
                    break;
                case Keys.Down:
                    game.tnk.MoveDown();
                    break;
                case Keys.Right:
                    game.tnk.MoveRight();
                    break;
                case Keys.Left:
                    game.tnk.MoveLeft();
                    break;
                case Keys.Space:
                    game.tnk.cmdShoot();
                    break;
            }
        }
        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            //tnk.StopMove();
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            game.Frame();
            Draw();
            if (game.IsGameOver())
            {
                timer1.Enabled = false;
                DialogResult testDialog = MessageBox.Show("Вы проиграли:(\nНачать новую игру?", "Проигрыш", MessageBoxButtons.YesNo);
                if (testDialog == DialogResult.Yes)
                {
                    game.GameObject.Clear();
                    LoadData loaddata = new LoadData();
                    game.InitGame(loaddata);
                    timer1.Enabled = true;
                }
                else
                {
                    this.Close();
                }

            }
            if (game.IsWin())
            {
                timer1.Enabled = false;
                DialogResult testDialog = MessageBox.Show("Вы выиграли:)\nНачать новую игру?", "Выигрыш", MessageBoxButtons.YesNo);
                if (testDialog == DialogResult.Yes)
                {
                    game.GameObject.Clear();
                    LoadData loaddata = new LoadData();
                    game.InitGame(loaddata);
                    timer1.Enabled = true;
                }
                else
                {
                    this.Close();
                }


            }
        }
        private void Draw()
        {
            int weight = Constants.pixel * Constants.tileWidth;
            System.Drawing.SolidBrush myBrush = new System.Drawing.SolidBrush(System.Drawing.Color.Red);
            System.Drawing.Graphics formGraphics;
            formGraphics = this.CreateGraphics();

            for (int i = 0; i < game.Map.GetRowNum(); i++)
                for (int j = 0; j < game.Map.GetColumnNum(); j++)
                {
                    TileBase iTile = game.Map.GetTile(i, j);
                    myBrush.Color = GetTileColor(iTile.Type());
                    formGraphics.FillRectangle(myBrush, new Rectangle(j * weight, i * weight, weight, weight));
                }
            foreach (MoveObject mobject in game.GameObject)
            {
                if (mobject is Tank)
                    myBrush.Color = Color.Green;
                else if (mobject is BonusSpeed)
                    myBrush.Color = Color.Pink;
                else if (mobject is BonusPower)
                    myBrush.Color = Color.Yellow;
                else if (mobject is Tower)
                    myBrush.Color = Color.Red;
                else if (mobject is TowerUpgrade)
                    myBrush.Color = Color.Violet;
                else
                    myBrush.Color = Color.Red;
                formGraphics.FillEllipse(myBrush, new Rectangle(mobject.X * Constants.pixel, mobject.Y * Constants.pixel, mobject.Width * Constants.pixel, mobject.Width * Constants.pixel));
                myBrush.Color = Color.White;
                if ((mobject is Tank) || (mobject is Tower) || (mobject is TowerUpgrade))
                {
                    if (mobject.Direction == DirectionType.Down)
                        formGraphics.FillRectangle(myBrush, new Rectangle((mobject.X + 1) * Constants.pixel + 5, (mobject.Y + 2) * Constants.pixel, Constants.pixel / 2, Constants.pixel));
                    if (mobject.Direction == DirectionType.Up)
                        formGraphics.FillRectangle(myBrush, new Rectangle((mobject.X + 1) * Constants.pixel + 5, mobject.Y * Constants.pixel, Constants.pixel / 2, Constants.pixel));
                    if (mobject.Direction == DirectionType.Left)
                        formGraphics.FillRectangle(myBrush, new Rectangle(mobject.X * Constants.pixel, (mobject.Y + 1) * Constants.pixel + 5, Constants.pixel, Constants.pixel / 2));
                    if (mobject.Direction == DirectionType.Right)
                        formGraphics.FillRectangle(myBrush, new Rectangle((mobject.X + 2) * Constants.pixel, (mobject.Y + 1) * Constants.pixel + 5, Constants.pixel, Constants.pixel / 2));
                }
            }
        }
    }
}

