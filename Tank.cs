using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kefir_BattleCity
{
	public class Tank: WarObject 
	{
		private DirectionType Position { get; set; }
        private bool tryShoot { get; set; }
        
		public Tank( int cellX, int cellY )
        {
            Width = Constants.tileWidth;
			X = cellX * Constants.tileWidth;
			Y = cellY * Constants.tileWidth;
			Direction = DirectionType.Up;
			Position  = DirectionType.None;
		    Weapon = new Weapon(14);
            HealthPoint = 3;
            tryShoot = false;
			Speed = 1;
		}

		public override void Move( Game game )
		{
				
			if( Position != DirectionType.None )
				Direction = Position;

            switch (Position)
            {
                case DirectionType.Down:
                    Y += Speed;
                    break;
                case DirectionType.Right:
                    X += Speed;
                    break;
                case DirectionType.Up:
                    Y -= Speed;
                    break;
                case DirectionType.Left:
                    X -= Speed;
                    break;
            }
            Position = DirectionType.None;


            if(tryShoot)
            {
                if (Weapon.CanShoot())
                {
                    Bullet bul = Weapon.Shoot();
                    bul.SetPosition(X, Y, Direction);
                    game.AddMoveObject(bul);
                }
            }
            tryShoot = false;
		}
		
		public void MoveUp()
		{
			Position = DirectionType.Up;
		}
		
		public void MoveDown()
		{
			Position = DirectionType.Down;
		}
		
		public void MoveRight()
		{
			Position = DirectionType.Right;
		}
		
		public void MoveLeft()
		{
			Position = DirectionType.Left;
		}
		
		public void StopMove()
		{
			Position = DirectionType.None;
		}
        public void cmdShoot()
        {
            tryShoot = true;
        }
        
	}
}
