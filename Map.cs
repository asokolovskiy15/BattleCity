using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kefir_BattleCity
{
	public enum TileType : int
	{
		Empty    = 0,
		Tower    = 1,
		Brick    = 2,
		Concrete = 3,
		Water    = 4,
        Border   = 5,
        TowerUpgrade=6
	}
	
	public class Map
	{
		private TileBase[,] _tiles;
		
		public void Load(int[,] description)
		{
			_tiles = new TileBase[ description.GetLength(0), description.GetLength(1) ];
			for(int row = 0; row < GetRowNum(); row++)
				for(int col = 0; col < GetColumnNum(); col++)
				{
					_tiles[row, col] = TileFactory( description[row, col] );
				}
		}
		
		public TileBase GetTile( int row, int col )
		{
            if (row < 0 || col < 0 || row >= GetRowNum() || col >= GetColumnNum())
                return null;
            return _tiles[row, col];
		}
		
		public void RemoveTile( TileBase tile )
		{
			for(int row = 0; row < GetRowNum(); row++)
				for(int col = 0; col < GetColumnNum(); col++)
				{
					if( _tiles[row, col] == tile )
					{
						_tiles[row, col] = TileFactory( TileType.Empty );
						return;
					}
				}

		}

		public int GetColumnNum()
		{
			return _tiles.GetLength(1);
		}
		
		public int GetRowNum()
		{
			return _tiles.GetLength(0);
		}
		
		public static TileBase TileFactory( int type )
		{
			return TileFactory( (TileType)type );
		}
		
		public static TileBase TileFactory( TileType type )
		{
			switch( type )
			{
				//case TileType.Tower:
				  //  return new Tower();
				case TileType.Brick:
					return new TileBrick();
				case TileType.Concrete:
					return new TileConcrete();
				case TileType.Water:
					return new TileWater();
				case TileType.Empty:
				default:
					return new TileEmpty();
			}
		}

		public void ChangeTile(int r, int c)
		{
			_tiles[r, c] = TileFactory( TileType.Empty );
		}
        public TileBase IntersectionTile(MoveObject mobject)
        {
            TileBase tile = null;

            int RowUp = mobject.Y / Constants.tileWidth; 
            int ColLeft = mobject.X / Constants.tileWidth; 
            int RowDown = (mobject.Y + mobject.Width - 1) / Constants.tileWidth; 
            int ColRight = (mobject.X + mobject.Width - 1) / Constants.tileWidth;

            for( int r = RowUp; r <= RowDown; ++r ) 
            { 
                for( int c = ColLeft; c <= ColRight; ++c ) 
                {
                    TileBase t = GetTile(r, c);
                    if (t == null)
                    {
                        if( tile == null )
                            tile = new TileBorder();
                        continue;
                    }

                    if ((t is TileWater && mobject is Bullet) || t is TileEmpty)
                        continue;

                    if (t.IsNeedRemove(mobject))
                        RemoveTile(t);

                    if (tile == null)
                        tile = t;
                }
            }
            return tile;
        }   
	}
}
