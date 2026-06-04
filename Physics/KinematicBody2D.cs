using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PalladiumEngine.Graphics;

namespace PalladiumEngine.Physics;

public class KinematicBody2D : CollisionShape
{
    public Sprite sprite { get; private set; }
    public Dictionary<string, Sprite> sprites { get; private set; }
    public Vector2 Velocity = Vector2.Zero;

    public KinematicBody2D() : base()
    {
	sprite = new Sprite();
    }

    public KinematicBody2D(Sprite sprite, CollisionShape hitBox) : base(hitBox)
    {
	this.sprite = sprite;
    }

    public KinematicBody2D(Dictionary<string, Sprite> sprites, CollisionShape hitBox, string defaultAnimation) : base(hitBox)
    {
	this.sprites = sprites;
	sprite = sprites[defaultAnimation];
    }

    public virtual void Initialize() { }

    public virtual void LoadContent() { }

    public virtual void Update(GameTime gameTime)
    {
	UpdatePos(Pos + Velocity);
	sprite.Update(gameTime);
    }

    public virtual void Draw(SpriteBatch spriteBatch)
    {
	sprite.Draw(spriteBatch, Pos);
    }

    public void Collide(CollisionShape other)
    {
	if (CheckCollision(other)) HandleCollision(other);
    }

    private void HandleCollision(CollisionShape other)
    {
	if (other.IsRect && IsRect)
	{
	    float overlapX = Rect.Width / 2 + other.Rect.Width / 2 - Math.Abs(Rect.Center.X - other.Rect.Center.X);
	    float overlapY = Rect.Height / 2 + other.Rect.Height / 2 - Math.Abs(Rect.Center.Y - other.Rect.Center.Y);

	    bool wasAbove = LastPos.Y + Rect.Height / 2 <= other.Rect.Top;
	    bool wasBelow = LastPos.Y >= other.Rect.Bottom;
	    bool wasLeft  = LastPos.X + Rect.Width / 2 <= other.Rect.Left;
	    bool wasRight = LastPos.X >= other.Rect.Right;

	    if (wasAbove || wasBelow)
	    {
		float direction = Velocity.Y > 0 ? -1 : 1;
		UpdatePos(new Vector2(Pos.X, Pos.Y + overlapY * direction));
	    }
	    else if (wasLeft || wasRight)
	    {
		float direction = Velocity.X > 0 ? -1 : 1;
		UpdatePos(new Vector2(Pos.X + overlapX * direction, Pos.Y));
	    }
	    else
	    {
		if (overlapX < overlapY)
		{
		    float direction = Velocity.X > 0 ? -1 : 1;
		    UpdatePos(new Vector2(Pos.X + overlapX * direction, Pos.Y));
		}
		else
		{
		    float direction = Velocity.Y > 0 ? -1 : 1;
		    UpdatePos(new Vector2(Pos.X, Pos.Y + overlapY * direction));
		}
	    }
	}
	else RevertToLastPos();
    }

    public void ChangeAnimation(string anim)
    {
	sprite = sprites[anim];
    }

    public virtual List<CollisionShape> GetCollideables() { return [this]; }
}
