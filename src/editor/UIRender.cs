using SDL2;

namespace Luna.Editor {

    public static class UIRender
    {
        public static void DrawBorder(IntPtr renderer, int x, int y, int w, int h, int thickness,
                                    byte r, byte g, byte b, byte a = 255)
        {
            SDL.SDL_SetRenderDrawColor(renderer, r, g, b, a);

            // Top
            SDL.SDL_Rect top = new() { x = x, y = y, w = w, h = thickness };
            SDL.SDL_RenderFillRect(renderer, ref top);

            // Bottom
            SDL.SDL_Rect bottom = new() { x = x, y = y + h - thickness, w = w, h = thickness };
            SDL.SDL_RenderFillRect(renderer, ref bottom);

            // Left
            SDL.SDL_Rect left = new() { x = x, y = y, w = thickness, h = h };
            SDL.SDL_RenderFillRect(renderer, ref left);

            // Right
            SDL.SDL_Rect right = new() { x = x + w - thickness, y = y, w = thickness, h = h };
            SDL.SDL_RenderFillRect(renderer, ref right);
        }
    }
}
