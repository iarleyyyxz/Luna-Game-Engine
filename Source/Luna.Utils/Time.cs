using SDL2;

namespace Luna.Util
{
    public static class Time
    {
        public static float DeltaTime { get; private set; }
        public static float FPS { get; private set; }

        private static int frameCount = 0;
        private static float elapsed = 0f;

        private static double lastTime = SDL.SDL_GetTicks() / 1000.0;

        public static void Update()
        {
            double now = SDL.SDL_GetTicks() / 1000.0;
            DeltaTime = (float)(now - lastTime);
            lastTime = now;

            frameCount++;
            elapsed += DeltaTime;

            if (elapsed >= 1.0f)
            {
                FPS = frameCount / elapsed;
                frameCount = 0;
                elapsed = 0f;
            }
        }
    }
}