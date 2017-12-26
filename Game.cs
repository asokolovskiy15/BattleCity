using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kefir_BattleCity
{
    public class Game
    {
        public Tank tnk;
        public BonusPower bns1;
        public BonusSpeed bns2;
        public Map Map { get; private set; }
        public List<MoveObject> GameObject { get; private set; }
        private static int countTick;
        private int score = 0;
        private int cntTower=0;
        public void InitGame(LoadData ld)
        {
            tnk = new Tank(6, 10);
            bns1 = new BonusPower(6, 9);
            bns2 = new BonusSpeed(9, 7);
            Map = new Map();
            Random rand = new Random();
            
            GameObject = new List<MoveObject>();
            
            AddMoveObject(tnk);
            AddMoveObject(bns1);
            AddMoveObject(bns2);
            Map.Load(ld.GetMapDescription());// G
            for (int r = 0; r < Map.GetRowNum(); r++)
                for (int c = 0; c < Map.GetColumnNum();c++ )
                {
                    if (ld.GetMapDescription()[r, c] == (int)TileType.Tower)
                    {
                       // rand.Next(0, 4);
                        AddMoveObject(new Tower(c,r,(DirectionType)rand.Next(0,4)));
                        cntTower++;
                    }
                    if (ld.GetMapDescription()[r, c] == (int)TileType.TowerUpgrade)
                    {
                        AddMoveObject(new TowerUpgrade(c, r));
                        cntTower++;
                    }
                }
           countTick = 0;
        }
        public bool Contact(MoveObject mobject1, MoveObject mobject2)
        {
            if(mobject1.X>= (mobject2.X + mobject2.Width) || (mobject1.X + mobject1.Width) <= mobject2.X) 
                return false;
        	if( mobject1.Y >= (mobject2.Y + mobject2.Width)|| (mobject1.Y + mobject1.Width) <= mobject2.Y )
                return false;
	        return true;

        }
        public void Frame()
        {           
            TileBase tile;
            int xold, yold;
            List<MoveObject> NumObjectForDel = new List<MoveObject>();// G
            for (int i = 0; i < GameObject.Count; i++)
            {
                xold = GameObject[i].X;
                yold = GameObject[i].Y;
                GameObject[i].Move(this);
                tile = Map.IntersectionTile(GameObject[i]);
                if (tile != null)
                {
                    bool needRemove;
                    GameObject[i].ContactTile(tile, out needRemove);
                    if (needRemove)
                        NumObjectForDel.Add(GameObject[i]);
                    else
                    {
                        GameObject[i].X = xold;
                        GameObject[i].Y = yold;
                    }
                    tile = null;
                }
                for (int j = i+1; j < GameObject.Count; j++)
                {
                    if (j != i)
                    {
                        bool needRemove;
                     if (Contact(GameObject[i], GameObject[j]))
                         {
                           if ((GameObject[j] is Bullet) && ((GameObject[i] is Tower)||(GameObject[i] is TowerUpgrade)))
                                 score++;
                           GameObject[i].ContactObject(GameObject[j], out needRemove);
                           if (needRemove)
                               NumObjectForDel.Add(GameObject[i]);
                           needRemove = false;
                           GameObject[j].ContactObject(GameObject[i], out needRemove);
                           if (needRemove)
                               NumObjectForDel.Add(GameObject[j]);
                         }
                    }
                }
            }
               foreach (MoveObject mobject in NumObjectForDel)
                for (int i = 0; i < GameObject.Count;i++)
                        if (mobject == GameObject[i])
                            GameObject.Remove(GameObject[i]);
                NumObjectForDel.Clear();
                countTick++;
               
        }
        public static int GetTick()
        {
            return countTick;
        }
        public void AddMoveObject(MoveObject mobject)
        {
            GameObject.Add(mobject);
        }
        public bool IsGameOver()
        {
            if (tnk.HealthPoint == 0)
                return true;
            else
                return false;
        }
        public bool IsWin()
        {

            if (score == cntTower)
                return true;
            else return false;
        }
        public int GetScore()
        {
            return score; 
        }
    }
}
