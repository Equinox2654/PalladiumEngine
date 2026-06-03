using System.Collections.Generic;
using System.Linq;

namespace PalladiumEngine.Physics;

public class CollisionHandler
{
    public List<CollisionShape> CollisionShapes = new List<CollisionShape>();

    public void Collide()
    {
	foreach (CollisionShape collisionShape in CollisionShapes)
	{
	    if (collisionShape is RayCast2D rayCast)
	    {
		List<CollisionShape> filteredList = CollisionShapes.Where(x => !(x is RayCast2D)).ToList();
		rayCast.Collide(filteredList);
	    }
	    else if (collisionShape is KinematicBody2D kinematicBody2D)
	    {
		foreach (CollisionShape collideable in CollisionShapes)
		{
		    if (collideable != kinematicBody2D) kinematicBody2D.Collide(collideable);
		}
	    }
	}
    }

    public void AddCollisionShapes(List<CollisionShape> list)
    {
	CollisionShapes.AddRange(list);
    }
}
