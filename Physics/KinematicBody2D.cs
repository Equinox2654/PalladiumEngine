using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PalladiumEngine.Graphics;

namespace PalladiumEngine.Physics;

public class KinematicBody2D
{
    public Sprite sprite { get; private set; }
    public Dictionary<string, AnimatedSprite> sprites { get; private set; }
    public CollisionShape hitBox { get; private set; }

    public Vector2 Velocity = Vector2.Zero;

    public KinematicBody2D()
    {
	sprite = new Sprite();
	hitBox = new CollisionShape();
    }

    public KinematicBody2D(Sprite sprite, CollisionShape hitBox)
    {
	this.sprite = sprite;
	this.hitBox = hitBox;
    }

    public KinematicBody2D(Dictionary<string, AnimatedSprite> sprites, CollisionShape hitBox, string defaultAnimation)
    {
	this.sprites = sprites;
	sprite = sprites[defaultAnimation];
	this.hitBox = hitBox;
    }

    public virtual void Initialize() { }

    public virtual void LoadContent() { }

    public virtual void Update(GameTime gameTime)
    {
	hitBox.UpdatePos(hitBox.Pos + Velocity);
	sprite.Update(gameTime);
    }

    public virtual void Draw(SpriteBatch spriteBatch)
    {
	sprite.Draw(spriteBatch, hitBox.Pos);
    }

    public void Collide(KinematicBody2D other)
    {
	if (hitBox.IsColliding(other.hitBox)) hitBox.RevertToLastPos();
    }

    public void ChangeAnimation(string anim)
    {
	sprite = sprites[anim];
    }
}
