namespace Luna.Editor
{
    public static class UIManager
    {
        public static int ScreenWidth { get; private set; }
        public static int ScreenHeight { get; private set; }

        // Lista de elementos UI
        private static readonly List<UIElement> elements = new();
        public static IReadOnlyList<UIElement> Elements => elements;

        /// <summary>
        /// Inicializa o UIManager com a resolução atual da janela.
        /// </summary>
        public static void Init(int width, int height)
        {
            ScreenWidth = width;
            ScreenHeight = height;
        }

        /// <summary>
        /// Adiciona um elemento UI ao sistema.
        /// </summary>
        public static void Add(UIElement el)
        {
            elements.Add(el);
            el.OnResize(); // força atualização inicial
        }

        /// <summary>
        /// Chamado sempre que a janela é redimensionada.
        /// </summary>
        public static void OnResize(int width, int height)
        {
            ScreenWidth = width;
            ScreenHeight = height;

            // Copia local para evitar modificação durante iteração
            var snapshot = elements.ToArray();

            foreach (var element in snapshot)
                element.OnResize();
        }

        /// <summary>
        /// Atualiza todos os elementos UI.
        /// </summary>
        public static void Update()
        {
            var snapshot = elements.ToArray();
            foreach (var element in snapshot)
                element.Update();
        }

        /// <summary>
        /// Renderiza todos os elementos UI.
        /// </summary>
        public static void Draw(IntPtr renderer)
        {
            var snapshot = elements.ToArray();
            foreach (var element in snapshot)
                if (element.Visible)
                    element.Draw(renderer);
        }
    }
}
