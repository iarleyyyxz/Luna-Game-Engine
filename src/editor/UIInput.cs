using Luna.IO;
using SDL2;

namespace Luna.Editor
{
    public class UIInput : UIElement
{
    public string Text = "";
    public bool Focused = false;

    public UIInput(string initial)
    {
        Text = initial;
        Width = 120;
        Height = 22;
    }

    public override void Update()
    {
        if (Mouse.IsClicked())
            Focused = (Mouse.X >= X && Mouse.Y >= Y && Mouse.X <= X + Width && Mouse.Y <= Y + Height);

        if (Focused)
        {
            Text += Keyboard.GetTextInput();

            if (Keyboard.IsKeyPressed(Keyboard.Key.Backspace) && Text.Length > 0)
                Text = Text.Substring(0, Text.Length - 1);
        }
    }

    public override void Draw(IntPtr r)
    {
        SDL.SDL_SetRenderDrawColor(r, 30, 30, 30, 255);
        SDL.SDL_Rect bg = new SDL.SDL_Rect { x = X, y = Y, w = Width, h = Height };
        SDL.SDL_RenderFillRect(r, ref bg);

       // SDL.SDL_SetRenderDrawColor(r, 200, 200, 200, 255);
      //  SDL.SDL_RenderDrawRect(r, ref bg);

        Font.Draw(r, Text == "" && !Focused ? "..." : Text, X + 5, Y + 3, 255, 255, 255);
    }
}

}