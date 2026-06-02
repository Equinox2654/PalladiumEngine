using System.Collections.Generic;

namespace PalladiumEngine.Physics;

public class CollisionHandler
{
    public List<CollisionShape> CollisionShapes = new List<CollisionShape>();

    public void Collide()
    {
	foreach (CollisionShape collisionShape in CollisionShapes)
	{
	    if (collisionShape is KinematicBody2D kinematicBody2D)
	    {
		foreach (CollisionShape collideable in CollisionShapes)
		{
		    if (collideable != kinematicBody2D) kinematicBody2D.Collide(collideable);
		}
	    }
	}
    }
}
