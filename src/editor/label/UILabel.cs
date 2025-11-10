using Luna.IO;

namespace Luna.Editor
{
    public class UILabel : UIElement
    {
        public int Size;
        public string Text;

        public UILabel(string text, int size = 16)
        {
            Text = text;
            Size = size;
            Width = Font.Width(text);
            Height = Font.Height(text);
        }

        public override void Draw(nint renderer)
        {
            // Desenha diretamente na posição relativa X, Y
            Font.Draw(renderer, Text, X, Y, 255, 255, 255);

            // Atualiza largura e altura para layout automático
            var (w, h) = Font.Measure(Text);
            Width = w;
            Height = h;
        }
    }
}
