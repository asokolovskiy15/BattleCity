using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kefir_BattleCity
{
public 	class Bullet: MoveObject
	{
		public int Power { get; protected set; }

		public Bullet( int power, int speed )
		{
            Width = 1;
			Power = power;
			Speed = speed;
		}
		
		public override void Move( Game game )
		{
			switch( Direction )
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
		}

		public void SetPosition( int cellX, int cellY, DirectionType direction )
		{
			Direction = direction;
			switch( Direction )
			{
				case DirectionType.Down:
					X = cellX + Constants.tileWidth / 2;
					Y = cellY + Constants.tileWidth;
					break;
				case DirectionType.Right:
					X = cellX + Constants.tileWidth;
					Y = cellY + Constants.tileWidth / 2;
					break;
				case DirectionType.Up:
					X = cellX + Constants.tileWidth / 2;
					Y = cellY-Width;
					break;
				case DirectionType.Left:
					X = cellX-Width;
					Y = cellY + Constants.tileWidth / 2;
					break;
			}
		}
        public override void ContactTile(TileBase tile, out bool needRemove)
        {
            needRemove = true;
        }
        public override void ContactObject(MoveObject mobject, out bool needRemove)
        {
            if (mobject is BonusBase)
                needRemove = false;
            else
                needRemove = true;
        }
	}
}
