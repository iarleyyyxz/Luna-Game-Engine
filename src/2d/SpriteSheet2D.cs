using System.Collections.Generic;
using System.Numerics;

namespace Luna.g2d
{
    public class SpriteSheet2D
    {
        public Texture2D Texture { get; private set; }
        public int FrameWidth { get; private set; }
        public int FrameHeight { get; private set; }

        public int TextureWidth => Texture.Width;
        public int TextureHeight => Texture.Height;

        public SpriteSheet2D(string path, int frameWidth, int frameHeight)
        {
            Texture = new Texture2D(path);
            FrameWidth = frameWidth;
            FrameHeight = frameHeight;
        }

        public Sprite2D GetFrame(int index)
        {
            int columns = TextureWidth / FrameWidth;
            int x = index % columns;
            int y = index / columns;

            float u0 = (x * FrameWidth) / (float)TextureWidth;
            float v0 = (y * FrameHeight) / (float)TextureHeight;
            float u1 = ((x + 1) * FrameWidth) / (float)TextureWidth;
            float v1 = ((y + 1) * FrameHeight) / (float)TextureHeight;

            Sprite2D sprite = new Sprite2D(Texture);
            sprite.UV0 = new Vector2(u0, v0);
            sprite.UV1 = new Vector2(u1, v1);
            sprite.Size = new Vector2(FrameWidth, FrameHeight);

            return sprite;
        }

        public List<Sprite2D> GetRange(int start, int end)
        {
            List<Sprite2D> frames = new();
            for (int i = start; i < end; i++)
                frames.Add(GetFrame(i));
            return frames;
        }
    }
}
