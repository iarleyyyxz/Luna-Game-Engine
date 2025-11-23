namespace Luna.Editor { 
    public abstract class UIElement
    {
        public int X, Y, Width, Height;
        public List<UIElement> Children = new();
        public bool Visible = true;

        /// <summary>
        /// If true, cannot change the position
        /// </summary>
        public bool ManualPosition = false;

        public abstract void Draw(IntPtr renderer);
        public virtual void Update() {}
        public virtual void OnResize() {}

        public void AddChild(UIElement element) => Children.Add(element);
    }
}
