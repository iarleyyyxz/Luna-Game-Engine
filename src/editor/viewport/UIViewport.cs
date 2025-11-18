using SDL2;
using OpenTK.Graphics.OpenGL4;
using Luna.Renderer;
using Luna.g2d.Scene;
using Luna.Util;

namespace Luna.Editor
{
    public class UIViewport : UIElement
    {
        FrameBuffer2D framebuffer;

        public UIViewport(nint renderer, int x, int y, int w, int h, FrameBuffer2D frm)
        {
            X = x; Y = y; Width = w; Height = h;
            framebuffer = frm;
        }

        public override void OnResize()
        {
            framebuffer.Resize(Width, Height);
        }

        public override void Draw(IntPtr renderer)
        {
            if (!Visible)
                return;

            // ----------------------------
            // 1. RENDER PARA O FRAMEBUFFER
            // ----------------------------
            framebuffer.Bind();

            GL.Viewport(0, 0, Width, Height);
            GL.ClearColor(0.15f, 0.15f, 0.15f, 1f);
            GL.Clear(ClearBufferMask.ColorBufferBit);

            if (!SceneManager.CurrentScene.Paused)
                SceneManager.CurrentScene.Update(Time.DeltaTime, Width, Height);

            framebuffer.Unbind();

            // -------------------------------------------
            // 2. TRANSFORMA O FRAMEBUFFER EM SDL TEXTURE
            // -------------------------------------------
            framebuffer.ReadToSDLTexture();

            // -------------------------------------------
            // 3. DESENHA A CENA NA TELA (SDL_RenderCopy)
            // -------------------------------------------
            SDL.SDL_Rect rect = new SDL.SDL_Rect
            {
                x = X,
                y = Y,
                w = Width,
                h = Height
            };

            SDL.SDL_RenderCopy(renderer, framebuffer.SDLTexture, IntPtr.Zero, ref rect);

            // -------------------------------------------
            // 4. DESENHAR BORDA *DEPOIS* DO SDL_RenderCopy
            // -------------------------------------------
            UIRender.DrawBorder(renderer, X, Y, Width, Height, 2, 255, 255, 255);

            // -------------------------------------------
            // 5. DESENHAR FILHOS (BOTÕES ETC)
            // -------------------------------------------
            foreach (var child in Children)
                child.Draw(renderer);
        }

        public override void Update()
        {
            foreach (var c in Children)
                c.Update();
        }
    }
}
