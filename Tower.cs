using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kefir_BattleCity
{
    class Tower : WarObject
		                   
    {
        public Tower( int cellX, int cellY, DirectionType direction )
        {
            Width = Constants.tileWidth;
			X = cellX * Constants.tileWidth;
			Y = cellY * Constants.tileWidth;
			Direction = direction;
			Weapon = new Weapon(22);
            HealthPoint = 1;
            Speed = 0;
		}
        public override void Move(Game game)
        {
          if (Weapon.CanShoot())
            {
               Bullet bul = Weapon.Shoot();
               bul.SetPosition(X, Y, Direction);
               game.AddMoveObject(bul);
            }     
        }
    }
}
