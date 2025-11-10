using Luna.IO;
using SDL2;
using System;
using System.Collections.Generic;

namespace Luna.Editor
{

    public class UICheckboxGroup : UIElement
    {
        public List<UICheckBox> Checkboxes = new();
        public Orientation Layout = Orientation.Vertical;
        public int Spacing = 5;

        public override void Update()
        {
            foreach (var cb in Checkboxes)
                cb.Update();
        }

        public override void Draw(IntPtr renderer)
        {
            int offsetX = 0;
            int offsetY = 0;

            foreach (var cb in Checkboxes)
            {
                cb.X = X + offsetX;
                cb.Y = Y + offsetY;
                cb.Layout = Layout;
                cb.Draw(renderer);

                if (Layout == Orientation.Horizontal)
                    offsetX += cb.Size + Font.Width(cb.Label) + Spacing;
                else
                    offsetY += cb.Size + Font.Height(cb.Label) + Spacing;
            }
        }
    }
}
