using Luna.Editor;
using Luna.IO;
using SDL2;

namespace Luna.Editor
{

    public class UICheckBox : UIElement
    {
        public int X, Y;
        public int Size = 20;

        public bool Checked;
        public Action<bool> OnChange; // <- callback

        private IntPtr texOn, texOff;

        public String Label;

        public Orientation Layout = Orientation.Horizontal;


        public UICheckBox(IntPtr checkedTex, IntPtr uncheckedTex, String Text)
        {
            texOn = checkedTex;
            texOff = uncheckedTex;
            Label = Text;
        }

        public override void Update()
        {
            if (Mouse.GetState(Mouse.Button.Left).Clicked)
            {
                if (Mouse.X >= X && Mouse.X <= X + Size &&
                    Mouse.Y >= Y && Mouse.Y <= Y + Size)
                {
                    Checked = !Checked;
                    OnChange?.Invoke(Checked);
                }
            }
        }

        public override void Draw(IntPtr renderer)
        {

            //    SDL.SDL_SetRenderDrawColor(renderer, 30, 30, 30, 255);
            //    SDL.SDL_Rect bg = new SDL.SDL_Rect { x = X, y = Y, w = Width, h = Height };
            //    SDL.SDL_RenderFillRect(renderer, ref bg);

            //   SDL.SDL_SetRenderDrawColor(renderer, 200, 200, 200, 255);
            //  SDL.SDL_RenderDrawRect(renderer, ref bg);
            SDL.SDL_Rect rect = new SDL.SDL_Rect { x = X, y = Y, w = Size, h = Size };

            if (Layout == Orientation.Horizontal)
            {
                // Checkbox à esquerda, texto à direita
                SDL.SDL_RenderCopy(renderer, Checked ? texOn : texOff, IntPtr.Zero, ref rect);
                Font.Draw(renderer, Label, X + Size + 5, Y + (Size / 2 - Font.Height(Label) / 2), 255, 255, 255);
            }
            else // Vertical
            {
                // Checkbox em cima, texto embaixo
                SDL.SDL_RenderCopy(renderer, Checked ? texOn : texOff, IntPtr.Zero, ref rect);
                Font.Draw(renderer, Label, X, Y + Size + 3, 255, 255, 255);
            }

        }
    }
}
