using SDL2;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Luna.IO
{
    public static class Font
    {
        private static IntPtr _font = IntPtr.Zero;
        private static IntPtr _renderer = IntPtr.Zero;

        private static Dictionary<string, IntPtr> _cache = new();
        private static Dictionary<string, (int w, int h)> _cacheSize = new();

        public static int FontSize;

        public static void Init(IntPtr sdlRenderer, string fontFile, int fontSize)
        {
            FontSize = fontSize;
            _renderer = sdlRenderer;

            if (SDL_ttf.TTF_Init() != 0)
                throw new Exception("Failed to init TTF: " + SDL.SDL_GetError());

            _font = SDL_ttf.TTF_OpenFont(fontFile, fontSize);
            if (_font == IntPtr.Zero)
                throw new Exception("Failed to load font: " + SDL.SDL_GetError());
        }

        public static IntPtr RenderText(string text, SDL.SDL_Color color, out int width, out int height)
        {
            string key = $"{text}_{color.r}_{color.g}_{color.b}_{color.a}";

            if (_cache.TryGetValue(key, out var cachedTex))
            {
                var size = _cacheSize[key];
                width = size.w;
                height = size.h;
                return cachedTex;
            }

            IntPtr surface = SDL_ttf.TTF_RenderUTF8_Blended(_font, text, color);
            if (surface == IntPtr.Zero)
            {
                width = height = 0;
                return IntPtr.Zero;
            }

            var surf = Marshal.PtrToStructure<SDL.SDL_Surface>(surface);
            width = surf.w;
            height = surf.h;

            IntPtr texture = SDL.SDL_CreateTextureFromSurface(_renderer, surface);
            SDL.SDL_FreeSurface(surface);

            _cache[key] = texture;
            _cacheSize[key] = (width, height);

            return texture;
        }

        public static void Draw(IntPtr renderer, string text, int x, int y, byte r = 255, byte g = 255, byte b = 255, byte a = 255)
        {
            if (string.IsNullOrEmpty(text)) return;

            var color = new SDL.SDL_Color { r = r, g = g, b = b, a = a };

            IntPtr tex = RenderText(text, color, out int w, out int h);
            if (tex == IntPtr.Zero) return;

            SDL.SDL_Rect dst = new() { x = x, y = y, w = w, h = h };
            SDL.SDL_RenderCopy(renderer, tex, IntPtr.Zero, ref dst);
        }

        public static (int w, int h) Measure(string text)
        {
            if (string.IsNullOrEmpty(text)) return (0, 0);
            SDL_ttf.TTF_SizeUTF8(_font, text, out int w, out int h);
            return (w, h);
        }

        public static int Width(string text) => Measure(text).w;
        public static int Height(string text) => Measure(text).h;

        public static void ClearCache()
        {
            foreach (var tex in _cache.Values)
                SDL.SDL_DestroyTexture(tex);

            _cache.Clear();
            _cacheSize.Clear();
        }

        public static void Quit()
        {
            ClearCache();

            if (_font != IntPtr.Zero)
            {
                SDL_ttf.TTF_CloseFont(_font);
                _font = IntPtr.Zero;
            }

            SDL_ttf.TTF_Quit();
        }
    }
}
