using Microsoft.Xna.Framework;

namespace PalladiumEngine.Physics;

public class CollisionShape
{
    public Rectangle Rect { get; private set; }
    public Circle Circ { get; private set; }
    private bool IsRect;
    public Vector2 LastPos { get; protected set; }
    public Vector2 Pos { get; protected set; }
    
    public int CollisionLayer { get; private set; }
    public int CollisionMask { get; private set; }

    public CollisionShape()
    {
	IsRect = true;
	Rect = new Rectangle();
	LastPos = Vector2.Zero;
	Pos = Vector2.Zero;
	CollisionLayer = 1;
	CollisionMask = 1;
    }

    public CollisionShape(CollisionShape collisionShape)
    {
	IsRect = collisionShape.GetHitbox() is Rectangle;
	Rect = collisionShape.Rect;
	Circ = collisionShape.Circ;
	LastPos = collisionShape.LastPos;
	Pos = collisionShape.Pos;
	CollisionLayer = collisionShape.CollisionLayer;
	CollisionMask = collisionShape.CollisionMask;
    }

    public CollisionShape(Circle c, int collisionLayer, int collisionMask)
    {
	IsRect = false;
	Circ = c;
	LastPos = Vector2.Zero;
	Pos = new Vector2(Circ.X, Circ.Y);
	CollisionLayer = collisionLayer;
	CollisionMask = collisionMask;
    }
    
    public CollisionShape(Rectangle r, int collisionLayer, int collisionMask)
    {
	IsRect = true;
	Rect = r;
	LastPos = Vector2.Zero;
	Pos = new Vector2(Rect.X, Rect.Y);
	CollisionLayer = collisionLayer;
	CollisionMask = collisionMask;
    }

    public object GetHitbox() => IsRect ? Rect : Circ;

    public bool IsColliding(CollisionShape other)
    {
	if (other.CollisionLayer == CollisionMask)
	{
	    if (other.IsRect && IsRect)
	    {
		return Rect.Intersects(other.Rect);
	    }
	    else if (!other.IsRect && !IsRect)
	    {
		return Circ.Intersects(other.Circ);
	    }
	    else if (other.IsRect && !IsRect)
	    {
		float testX = MathHelper.Clamp(Circ.X, other.Rect.Left, other.Rect.Right);
		float testY = MathHelper.Clamp(Circ.Y, other.Rect.Top, other.Rect.Bottom);

		Vector2 distance = new Vector2(
		    Circ.X - testX,
		    Circ.Y - testY
		);

		float distanceSquared = distance.X*distance.X + distance.Y*distance.Y;

		return distanceSquared <= Circ.Radius*Circ.Radius;
	    }
	    else if (!other.IsRect && IsRect)
	    {
		Vector2 closestPoint = new Vector2(
		    MathHelper.Clamp(other.Circ.X, Rect.Left, Rect.Right),
		    MathHelper.Clamp(other.Circ.Y, Rect.Top, Rect.Bottom)
		);
		
		Vector2 distance = new Vector2(
		    other.Circ.X - closestPoint.X,
		    other.Circ.Y - closestPoint.Y
		);

		float distanceSquared = distance.X*distance.X + distance.Y*distance.Y;

		return distanceSquared <= other.Circ.Radius*other.Circ.Radius;
	    }
	}
	return false;
    }

    public void RevertToLastPos() => Pos = LastPos;
    public void UpdatePos(Vector2 newPos)
    {
	LastPos = Pos;
	Pos = newPos;
	if (IsRect) Rect = new Rectangle((int)newPos.X, (int)newPos.Y, Rect.Width, Rect.Height);
	else Circ = new Circle((int)newPos.X, (int)newPos.Y, Circ.Radius);
    }
}
