using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kefir_BattleCity
{
    public class TowerUpgrade:WarObject
    {
        public TowerUpgrade( int cellX, int cellY )
        {
            Width = Constants.tileWidth;
			X = cellX * Constants.tileWidth;
			Y = cellY * Constants.tileWidth;
			Weapon = new Weapon(19);
            HealthPoint = 2;
            Speed = 0;
		}
        public override void Move(Game game)
        {
            if (game.tnk.Y + 3 > Y && game.tnk.Y < Y +3)
            {
                Direction = game.tnk.X >= X ? DirectionType.Right : DirectionType.Left;
            }
            else /* Vertical direction */
            {
                Direction = game.tnk.Y < Y + 3 ? DirectionType.Up : DirectionType.Down;
            }
                   if (Weapon.CanShoot())
                {
                    Bullet bul = Weapon.Shoot();
                    bul.SetPosition(X, Y, Direction);
                    game.AddMoveObject(bul);
                }            
        }
    }
}
