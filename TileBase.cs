using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kefir_BattleCity
{
    public abstract class TileBase 
    {
        public abstract TileType Type();
        public virtual bool IsNeedRemove(MoveObject mobject)
        {
            return false;
        }
    }
}
