using SDL2;

namespace Luna.Editor
{
    public class UIButton : UIElement
    {

        public String Text;

        public override void Draw(IntPtr renderer)
        {
            if (!Visible) return;

            SDL.SDL_Rect rect = new SDL.SDL_Rect { x = X, y = Y, w = Width, h = Height };
            SDL.SDL_SetRenderDrawColor(renderer, 50, 50, 50, 255);
            SDL.SDL_RenderFillRect(renderer, ref rect);
        }
    }
}