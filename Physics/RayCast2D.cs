using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace PalladiumEngine.Physics;

public class RayCast2D : CollisionShape
{
    public bool IsColliding { get; private set; }

    public RayCast2D(Vector2 Pos) : base(
	    new Circle(
		(int)Pos.X,
		(int)Pos.Y,
		2
	    ),
	    1,
	    1
	)
    {
	IsColliding = false;
    }

    public RayCast2D(Vector2 Pos, int collisionLayer) : base(
	    new Circle(
		(int)Pos.X,
		(int)Pos.Y,
		2
	    ),
	    collisionLayer,
	    1
	)
    {
	IsColliding = false;
    }

    public void Collide(List<CollisionShape> others)
    {
	UpdatePos(Pos);
	foreach (CollisionShape other in others)
	{
	    if (!(other is RayCast2D))
	    {
		if (CheckCollision(other))
		{
		    IsColliding = true;
		    return;
		}
	    }
	}
	IsColliding = false;
    }
}
