using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kefir_BattleCity
{
    public class Weapon
    {
        public bool Type { get; set; }
        public Weapon(int _interval)
        {
            interval = _interval;
            Type = false;
        }
        public int interval;
        private int lastShoot;
        public bool CanShoot() 
        {
            if(Game.GetTick()-lastShoot>interval)
                return true;
            return false;
        }
        public Bullet Shoot()
        {
            if (CanShoot())
            {
                lastShoot = Game.GetTick();
                if(!Type)
                    return new Bullet(1, 1);
                else
                    return new Bullet(2, 1);
            }
            else
                return null;
        }
    }
}
