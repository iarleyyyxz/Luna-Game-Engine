using Luna.g2d;
using Luna.g2d.Renderer;
using System.Numerics;

namespace Luna.Ecs
{
    public class SpriteRenderer : Component
    {
        public Sprite2D Sprite;
        private Transform2D transform;

        public override void Start()
        {
            transform = owner.GetComponent<Transform2D>() ?? owner.AddComponent<Transform2D>();
        }

        public override void Update(float dt)
        {
            if (Sprite == null || Sprite.Texture == null) return;

            // ensure transform reference
            if (transform == null) transform = owner.GetComponent<Transform2D>();

            // Remove subpixel jitter by rounding position (common for pixel-art)
            Sprite.Position = new Vector2(
                MathF.Round(transform.Position.X),
                MathF.Round(transform.Position.Y)
            );

            Sprite.Size = new Vector2(transform.Scale.X, transform.Scale.Y);

            Sprite.Rotation = transform.Rotation;

            // queue for the renderer (draw will happen in the batch)
            Renderer2D.Draw(Sprite);
        }
    }
}
