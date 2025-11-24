using Luna.IO;
using SDL2;
using System.Collections.Generic;

namespace Luna.Editor
{
    public class UIPanel : UIElement
    {
        public string Title = "Panel";
        public UICheckboxGroup CheckboxGroup = new();

        public int Padding = 8;
        public int Spacing
        {
            get => CheckboxGroup.Spacing;
            set => CheckboxGroup.Spacing = value;
        }
        public Orientation Layout
        {
            get => CheckboxGroup.Layout;
            set => CheckboxGroup.Layout = value;
        }

        private bool dragging;
        private int dragOffsetX, dragOffsetY;

        public override void Update()
        {
            if (Mouse.GetState(Mouse.Button.Left).Clicked)
            {
                if (Mouse.X >= X && Mouse.X <= X + Width &&
                    Mouse.Y >= Y && Mouse.Y <= Y + 24)
                {
                    dragging = true;
                    dragOffsetX = Mouse.X - X;
                    dragOffsetY = Mouse.Y - Y;
                }
            }

            if (Mouse.GetState(Mouse.Button.Left).Clicked && dragging)
            {
                X = Mouse.X - dragOffsetX;
                Y = Mouse.Y - dragOffsetY;
            }

            if (Mouse.GetState(Mouse.Button.Left).Released)
                dragging = false;

            // Atualiza todos os filhos
            int offsetX = Padding;
            int offsetY = 40; // abaixo do título
    

            foreach (var child in Children)
            {
                child.X = X + offsetX;
                child.Y = Y + offsetY;
                child.Update();
                

                // Ajusta offset vertical ou horizontal dependendo do tipo
                if (child is UICheckboxGroup group)
                {
                    offsetY += group.Checkboxes.Count * (group.Spacing + 20); // 20 = tamanho médio checkbox
                }
                else    offsetY += 30; // altura média para outros elementos
                }
                {
                
            }
    
        }

        public override void Draw(IntPtr renderer)
        {
            // Fundo do painel
            // Fundo do painel
            SDL.SDL_SetRenderDrawColor(renderer, 18, 18, 22, 255);
            SDL.SDL_Rect bg = new() { x = X, y = Y, w = Width, h = Height };
            SDL.SDL_RenderFillRect(renderer, ref bg);

            // Título
            Font.Draw(renderer, Title, X + 8, Y + 6, 200, 200, 220);

            // Linha separadora
            SDL.SDL_SetRenderDrawColor(renderer, 90, 90, 110, 255);
            SDL.SDL_RenderDrawLine(renderer, X + 4, Y + 28, X + Width - 4, Y + 28);

            // Desenha todos os filhos
            foreach (var child in Children)
                child.Draw(renderer);
        }
    
    }
}
