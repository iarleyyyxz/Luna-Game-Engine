using Luna.IO;
using SDL2;
using System;

namespace Luna.Editor
{
    public class UIButton : UIElement
    {
        public string Text { get; set; } = string.Empty;

        public IntPtr IconNormal = IntPtr.Zero;
        public IntPtr IconHover = IntPtr.Zero;
        public IntPtr IconPressed = IntPtr.Zero;

        public bool UseIconOnly => Text == string.Empty;

        public Action? OnClick;
        public Action? OnHover;

        public bool IsMouseHovered = false;
        public bool IsPressed = false;

        public UIButton(string text = "")
        {
            Text = text;
            SDL.SDL_SetHint(SDL.SDL_HINT_RENDER_SCALE_QUALITY, "2"); // HIGH QUALITY
        }

        public void SetIcon(nint renderer, string path)
        {
            IconNormal = Load(renderer, path);
            IconHover = IconNormal;
            IconPressed = IconNormal;
        }

        public void SetIcons(nint renderer, string normal, string hover, string pressed)
        {
            IconNormal = Load(renderer, normal);
            IconHover = Load(renderer, hover);
            IconPressed = Load(renderer, pressed);
        }

        private IntPtr Load(nint renderer, string path)
        {
            IntPtr tex = SDL_image.IMG_LoadTexture(renderer, path);
            if (tex == IntPtr.Zero)
                throw new Exception("Failed to load icon: " + SDL.SDL_GetError());
            return tex;
        }

        public override void Draw(IntPtr renderer)
        {
            if (!Visible) return;

            // ==== BACKGROUND ====
            byte bg = (byte)(IsPressed ? 40 : IsMouseHovered ? 65 : 50);
            SDL.SDL_SetRenderDrawColor(renderer, bg, bg, bg, 255);

            SDL.SDL_Rect rect = new SDL.SDL_Rect { x = X, y = Y, w = Width, h = Height };
            SDL.SDL_RenderFillRect(renderer, ref rect);

            // ==== BORDER ====
            SDL.SDL_SetRenderDrawColor(renderer, 25, 25, 25, 255);
            SDL.SDL_RenderDrawRect(renderer, ref rect);

            // === Choose icon ===
            IntPtr icon = IsPressed ? IconPressed :
                          IsMouseHovered ? IconHover :
                          IconNormal;

            int offsetX = X + 6;

            // ======== DRAW ICON — FIXED VERSION ========
            if (icon != IntPtr.Zero)
            {
                SDL.SDL_QueryTexture(icon, out _, out _, out int iw, out int ih);

                // Fit the ICON to the button, keeping aspect ratio — BEST FIX
                float scale = MathF.Min((float)Width / iw, (float)Height / ih);

                int drawW = (int)(iw * scale);
                int drawH = (int)(ih * scale);

                SDL.SDL_Rect iconRect = new SDL.SDL_Rect
                {
                    x = X + (Width - drawW) / 2,
                    y = Y + (Height - drawH) / 2,
                    w = drawW,
                    h = drawH
                };

                SDL.SDL_RenderCopy(renderer, icon, IntPtr.Zero, ref iconRect);

                if (UseIconOnly)
                    return;

                offsetX = X + drawW + 10;
            }

            // ======== DRAW TEXT ========
            if (!string.IsNullOrEmpty(Text))
            {
                SDL.SDL_Color white = new SDL.SDL_Color { r = 255, g = 255, b = 255, a = 255 };

                IntPtr txt = Font.RenderText(Text, white, out int tw, out int th);

                SDL.SDL_Rect textRect = new SDL.SDL_Rect
                {
                    x = UseIconOnly ? X + (Width - tw) / 2 : offsetX,
                    y = Y + (Height - th) / 2,
                    w = tw,
                    h = th
                };

                SDL.SDL_RenderCopy(renderer, txt, IntPtr.Zero, ref textRect);
                SDL.SDL_DestroyTexture(txt);
            }
        }

        public override void Update()
        {
            if (!Visible) return;

            bool inside =
                Mouse.X >= X && Mouse.Y >= Y &&
                Mouse.X < X + Width && Mouse.Y < Y + Height;

            if (inside && !IsMouseHovered)
                OnHover?.Invoke();

            IsMouseHovered = inside;

            var mouse = Mouse.GetState(Mouse.Button.Left);

            // Press
            if (inside && mouse.Clicked && !IsPressed)
                IsPressed = true;

            // Release
            if (IsPressed && !mouse.Clicked)
            {
                if (inside)
                    OnClick?.Invoke();

                IsPressed = false;
            }
        }
    }
}
