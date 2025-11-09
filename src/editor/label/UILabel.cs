using Luna.IO;

namespace Luna.Editor
{
    public class UILabel : UIElement
    {

        public int Size;
        public string Label;

        public UILabel(string label, int size)
        {
            Label = label;
            Size = size;
        }
        public override void Draw(nint renderer)
        {
            Font.Draw(renderer, Label, X, Y + Size + 3, 255, 255, 255);
        }
    }
}