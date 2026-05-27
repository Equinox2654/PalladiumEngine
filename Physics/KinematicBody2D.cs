using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PalladiumEngine.Graphics;

namespace PalladiumEngine.Physics;

public class KinematicBody2D
{
    public Sprite sprite { get; private set; }
    public readonly AnimatedSprite[] sprites;
    public readonly CollisionShape hitBox;

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

    public KinematicBody2D(AnimatedSprite[] sprites, CollisionShape hitBox)
    {
	this.sprites = sprites;
	sprite = sprites[0];
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

    public void ChangeAnimation(int anim)
    {
	sprite = sprites[anim];
    }
}
