using Luna.g2d.Scene;
using SDL2;

namespace Luna.Editor
{
    public class PlayPauseBar : UIElement
    {
        private UIButton playButton;
        private UIButton pauseButton;

        public PlayPauseBar(IntPtr playTex, IntPtr pauseTex, int x, int y)
        {
            X = x;
            Y = y;
            Width = 100;
            Height = 48;

            // Botão de Play (48x48)
            playButton = new UIButton();
            playButton.Width = 48;
            playButton.Height = 48;
            playButton.X = X;
            playButton.Y = Y;
            playButton.IconNormal = playTex;
            playButton.IconHover = playTex;
            playButton.IconPressed = playTex;
            playButton.OnClick = () => SceneManager.CurrentScene.Paused = false;

            // Botão de Pause
            pauseButton = new UIButton();
            pauseButton.Width = 48;
            pauseButton.Height = 48;
            pauseButton.X = X + 52;
            pauseButton.Y = Y;
            pauseButton.IconNormal = pauseTex;
            pauseButton.IconHover = pauseTex;
            pauseButton.IconPressed = pauseTex;
            pauseButton.OnClick = () => SceneManager.CurrentScene.Paused = true;

            AddChild(playButton);
            AddChild(pauseButton);
        }

        public override void Draw(IntPtr renderer)
        {
            if (!Visible) return;

            foreach (var c in Children)
                c.Draw(renderer);
        }

        public override void Update()
        {
            if (!Visible) return;

            foreach (var c in Children)
                c.Update();
        }

        public override void OnResize()
        {
            // Mantém a barra sempre no topo, centralizada (opcional)
            // X = 20;  // ou forma dinâmica
            // Y = 20;

            playButton.X = X;
            playButton.Y = Y;

            pauseButton.X = X + 52;
            pauseButton.Y = Y;
        }
    }
}
