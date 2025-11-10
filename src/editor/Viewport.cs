using Luna.IO;
using SDL2;

namespace Luna.Editor
{
    public class Viewport : UIElement
    {
        public string Title { get; set; } = "Viewport";

        public override void Update()
        {
            // Depois pode ter seleção, drag, gizmos etc
        }

        public override void Draw(IntPtr renderer)
        {
            // 1. Painel fundo
            SDL.SDL_SetRenderDrawColor(renderer, 30, 30, 35, 255);
            SDL.SDL_Rect panel = new SDL.SDL_Rect { x = X, y = Y, w = Width, h = Height };
            SDL.SDL_RenderFillRect(renderer, ref panel);

            // 2. Barra do topo (título)
            SDL.SDL_SetRenderDrawColor(renderer, 45, 45, 50, 255);
            SDL.SDL_Rect topBar = new SDL.SDL_Rect { x = X, y = Y, w = Width, h = 22 };
            SDL.SDL_RenderFillRect(renderer, ref topBar);

            // 3. Renderizar título
            int tw, th;
            var white = new SDL.SDL_Color { r = 200, g = 200, b = 200, a = 255 };
            var titleTex = Font.RenderText(Title, white, out tw, out th);

            SDL.SDL_Rect titleRect = new SDL.SDL_Rect
            {
                x = X + 6,
                y = Y + 3,
                w = tw,
                h = th
            };

            SDL.SDL_RenderCopy(renderer, titleTex, IntPtr.Zero, ref titleRect);
            SDL.SDL_DestroyTexture(titleTex);

            // 4. Definir área onde a cena será desenhada
            // VIEWPORT: cena no editor
            SDL.SDL_Rect sceneArea = new SDL.SDL_Rect
            {
                x = X + 2,
                y = Y + 24,
                w = Width - 4,
                h = Height - 26
            };

            SDL.SDL_RenderSetViewport(renderer, ref sceneArea);

            // desenha cena (o quadrado laranja)
            DrawScene(renderer);

// ✅ VOLTA PARA VIEWPORT FULLSCREEN PARA DESENHAR UI
            SDL.SDL_Rect fullscreenViewport = new SDL.SDL_Rect
            {
                x = 0,
                y = 0,
                w = 800,
                h = 600
            };

            SDL.SDL_RenderSetViewport(renderer, ref fullscreenViewport);


            // 7. Borda do painel
            SDL.SDL_SetRenderDrawColor(renderer, 60, 60, 65, 255);
            SDL.SDL_RenderDrawRect(renderer, ref panel);
        }

        private void DrawScene(IntPtr renderer)
        {
            // Exemplo: fundo do jogo no viewport
            SDL.SDL_SetRenderDrawColor(renderer, 20, 20, 22, 255);
            SDL.SDL_RenderClear(renderer);

            // Exemplo: um quadrado no centro do viewport
            SDL.SDL_SetRenderDrawColor(renderer, 200, 120, 50, 255);
            SDL.SDL_Rect box = new SDL.SDL_Rect { x = 100, y = 80, w = 120, h = 120 };
            SDL.SDL_RenderFillRect(renderer, ref box);
        }
    }
}
