using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kefir_BattleCity
{
     public class BonusSpeed:BonusBase
    {
         public BonusSpeed( int cellX, int cellY )
        {
            Width = Constants.tileWidth;
			X = cellX * Constants.tileWidth;
			Y = cellY * Constants.tileWidth;
            Speed = 0;
            
		}
         public override void ContactObject(MoveObject mobject, out bool needRemove)
         {
             if (mobject is Tank)
             {
                 (mobject as Tank).Weapon.interval = 3;
                 needRemove = true;
             }
             else
                 needRemove = false;
         }
    }
}
