using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kefir_BattleCity
{
	public enum DirectionType : int
	{
		Down  = 0,
		Up    = 1,
		Left  = 2,
		Right = 3,
		None  = 4
	}
	
    public abstract class MoveObject
	{
		public int X { get; set; }
		public int Y { get; set; }
        public int Width { get; protected set; }
		public int Speed { get; set; }
		
		public DirectionType Direction { get; set; }
		

		public virtual void Move(Game game)
		{
			
		}

        public virtual void ContactTile(TileBase tile,out bool needRemove)
        {
            needRemove = false;
        }
        public virtual void ContactObject(MoveObject mobject, out bool needRemove)
        {
            needRemove = false; 
        }
	}
}
                                