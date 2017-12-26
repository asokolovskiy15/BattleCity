using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kefir_BattleCity
{
    class TileEmpty : TileBase
    {
        public override TileType Type()
        {
            return TileType.Empty;
        }
    }
}
