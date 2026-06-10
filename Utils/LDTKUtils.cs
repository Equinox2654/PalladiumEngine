using LDtk;
using Microsoft.Xna.Framework;
using PalladiumEngine.Physics;
using System.Collections.Generic;

public static class Utils
{
    static List<StaticBody2D> ParseIntGridColliders(LDtkLevel level, int layer)
    {
	List<StaticBody2D> colliders = new List<StaticBody2D>();

	LDtkIntGrid intGrid= (LDtkIntGrid)level.LayerInstances.GetValue(layer);

	if (intGrid!= null)
	{
	    Point Pos = Point.Zero;
	    foreach (int Value in intGrid.Values)
	    {
		if (Value != 0)
		{
		    Pos.X += intGrid.TileSize;
		    if (Pos.X / intGrid.TileSize > intGrid.GridSize.X)
		    {
			Pos.X = 0;
			Pos.Y += intGrid.TileSize;
		    }
		    colliders.Add(new StaticBody2D(new Rectangle(Pos.X + intGrid.WorldPosition.X, Pos.Y + intGrid.WorldPosition.Y, intGrid.TileSize, intGrid.TileSize)));
		}
	    }
	}

	return colliders;
    }
}
