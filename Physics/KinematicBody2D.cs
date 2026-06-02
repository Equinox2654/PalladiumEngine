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
	if (IsColliding(other)) HandleCollision();
    }

    private void HandleCollision()
    {
	RevertToLastPos();
    }

    public void ChangeAnimation(string anim)
    {
	sprite = sprites[anim];
    }
}
