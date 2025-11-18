using SDL2;
using Luna.IO;

namespace Luna.Editor
{
    public class Menu : UIElement
    {
        public string Title = "Menu";
        private List<MenuItem> _items = new();
        public bool IsOpen = false;

        public void AddMenuItem(MenuItem item) => _items.Add(item);

        public override void Draw(IntPtr renderer)
        {
            var white = new SDL.SDL_Color { r = 255, g = 255, b = 255, a = 255 };

            IntPtr text = Font.RenderText(Title, white, out int w, out int h);
            SDL.SDL_Rect textRect = new SDL.SDL_Rect
            {
                x = X + (Width / 2) - (w / 2),
                y = Y + (Height / 2) - (h / 2),
                w = w,
                h = h
            };
            SDL.SDL_RenderCopy(renderer, text, IntPtr.Zero, ref textRect);
            SDL.SDL_DestroyTexture(text);

            if (!IsOpen) return;

            int itemHeight = 26;
            for (int i = 0; i < _items.Count; i++)
            {
                int itemY = Y + Height + (i * itemHeight);

                SDL.SDL_Rect box = new SDL.SDL_Rect { x = X, y = itemY, w = Width, h = itemHeight };
                SDL.SDL_SetRenderDrawColor(renderer, 30, 30, 30, 255);
                SDL.SDL_RenderFillRect(renderer, ref box);

                IntPtr itemText = Font.RenderText(_items[i].Title, white, out int iw, out int ih);
                SDL.SDL_Rect itemRect = new SDL.SDL_Rect
                {
                    x = X + 6,
                    y = itemY + (itemHeight / 2) - (ih / 2),
                    w = iw,
                    h = ih
                };

                SDL.SDL_RenderCopy(renderer, itemText, IntPtr.Zero, ref itemRect);
                SDL.SDL_DestroyTexture(itemText);
            }
        }

        public override void Update()
        {
            bool hoveredTop = Mouse.IsMouseOver(Mouse.X, Mouse.Y, X, Y, Width, Height);

            if (hoveredTop && Mouse.GetState(Mouse.Button.Left).Clicked)
                IsOpen = !IsOpen;

            if (!IsOpen)
                return;

            int itemHeight = 26;
            for (int i = 0; i < _items.Count; i++)
            {
                int iy = Y + Height + (i * itemHeight);
                if (Mouse.IsMouseOver(Mouse.X, Mouse.Y, X, iy, Width, itemHeight)
                    && Mouse.GetState(Mouse.Button.Left).Clicked)
                {
                    _items[i].Trigger();
                    IsOpen = false;
                }
            }

            bool insideDropdown = Mouse.IsMouseOver(Mouse.X, Mouse.Y, X, Y + Height, Width, _items.Count * itemHeight);
            if (!hoveredTop && !insideDropdown && Mouse.GetState(Mouse.Button.Left).Clicked)
                IsOpen = false;
        }
    }
}
