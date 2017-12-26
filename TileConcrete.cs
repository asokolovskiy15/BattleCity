using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kefir_BattleCity
{
    class TileConcrete: TileBase
    {
        public override TileType Type()
        {
            return TileType.Concrete;
        }
        public override bool IsNeedRemove(MoveObject mobject)
        {
            var bul = mobject as Bullet;
            if(bul != null && bul.Power > 1)
                return true;
            else
                return false;
        }
    }
}
