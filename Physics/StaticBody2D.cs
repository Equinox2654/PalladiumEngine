using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PalladiumEngine.Graphics;

namespace PalladiumEngine.Physics;

public class StaticBody2D : CollisionShape
{
    public Sprite sprite { get; private set; }
    public CollisionShape hitBox { get; private set; }
    
    public StaticBody2D() : base()
    {
	sprite = new Sprite();
    }

    public StaticBody2D(Sprite sprite) : base()
    {
	this.sprite = sprite;
    }

    public StaticBody2D(Sprite sprite, Vector2 Pos) : base(
	new Rectangle(
	    (int)Pos.X,
	    (int)Pos.Y,
	    (int)sprite.Width,
	    (int)sprite.Height
	),
	1,
	1
    )
    {
	this.sprite = sprite;
    }

    public StaticBody2D(Sprite sprite, Vector2 Pos, int collisionLayer) : base(
	new Rectangle(
	    (int)Pos.X,
	    (int)Pos.Y,
	    (int)sprite.Width,
	    (int)sprite.Height
	    ),
	collisionLayer,
	collisionLayer
    )
    {
	this.sprite = sprite;
    }

    public StaticBody2D(Sprite sprite, CollisionShape hitBox) : base(hitBox)
    {
	this.sprite = sprite;
    }

    public StaticBody2D(CollisionShape hitBox) : base(hitBox) { }

    public StaticBody2D(Rectangle rect) : base(rect, 1, 1) { }

    public virtual void Update(GameTime gameTime) => sprite.Update(gameTime);

    public virtual void Draw() => sprite.Draw(Pos);
}
