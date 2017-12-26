using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kefir_BattleCity
{
    class TileBrick: TileBase
    {
        public override TileType Type()
        {
        	return TileType.Brick;
        }
        public override bool IsNeedRemove(MoveObject mobject)
        {
            if (mobject is Bullet)
                return true;
            else
                return false;
        }
    }
}
