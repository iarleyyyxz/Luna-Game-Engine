using SDL2;

namespace Luna.IO
{

    public static class Keyboard
    {
        // Keyboard related methods and properties would go here

        public enum Key
        {
            A, B, C, D, E, F, G, H, I, J, K, L, M, N, O, P, Q, R, S, T, U, V, W, X, Y, Z,
            Num0, Num1, Num2, Num3, Num4, Num5, Num6, Num7, Num8, Num9,
            Escape, Space, Enter, Shift, Control, Alt,
            LeftArrow, RightArrow, UpArrow, DownArrow,
            Plus, Minus, Equals, Backspace, Tab, CapsLock, Delete,
            F1, F2, F3, F4, F5, F6, F7, F8, F9, F10, F11, F12,
            LShift, AltGr, Insert, Home, End, PageUp, PageDown,
            NumLock, ScrollLock, PrintScreen
        };

        private static Dictionary<Key, bool> _keyPressed = new();
        private static Dictionary<Key, bool> _keyReleased = new();
        private static Dictionary<Key, bool> _keyHeld = new();

        static Keyboard()
        {
            foreach (Key key in Enum.GetValues(typeof(Key)))
            {
                _keyPressed[key] = false;
                _keyReleased[key] = false;
                _keyHeld[key] = false;
            }   
        }

        public static void ProcessEvent(SDL.SDL_Event e)
        {
            if (e.type == SDL.SDL_EventType.SDL_KEYDOWN && e.key.repeat == 0)
            {
                var key = MapSDLKey(e.key.keysym.sym);
                if (key.HasValue)
                {
                    _keyPressed[key.Value] = true;
                    _keyHeld[key.Value] = true;
                }
            }

            if (e.type == SDL.SDL_EventType.SDL_KEYUP)
            {
                var key = MapSDLKey(e.key.keysym.sym);
                if (key.HasValue)
                {
                    _keyReleased[key.Value] = true;
                    _keyHeld[key.Value] = false;
                }
            }
        }

        public static void Update()
        {
            foreach (var key in _keyPressed.Keys.ToList())
            {
                _keyPressed[key] = false;
                _keyReleased[key] = false;
            }
        }

        public static bool IsKeyPressed(Key key)  => _keyPressed[key];
        public static bool IsKeyHeld(Key key)     => _keyHeld[key];
        public static bool IsKeyReleased(Key key) => _keyReleased[key];

        private static Key? MapSDLKey(SDL.SDL_Keycode code)
        {
            return code switch
            {
            SDL.SDL_Keycode.SDLK_a => Key.A,
            SDL.SDL_Keycode.SDLK_b => Key.B,
            SDL.SDL_Keycode.SDLK_c => Key.C,
            SDL.SDL_Keycode.SDLK_d => Key.D,
            SDL.SDL_Keycode.SDLK_e => Key.E,
            SDL.SDL_Keycode.SDLK_f => Key.F,
            SDL.SDL_Keycode.SDLK_g => Key.G,
            SDL.SDL_Keycode.SDLK_h => Key.H,
            SDL.SDL_Keycode.SDLK_i => Key.I,
            SDL.SDL_Keycode.SDLK_j => Key.J,
            SDL.SDL_Keycode.SDLK_k => Key.K,
            SDL.SDL_Keycode.SDLK_l => Key.L,
            SDL.SDL_Keycode.SDLK_m => Key.M,
            SDL.SDL_Keycode.SDLK_n => Key.N,
            SDL.SDL_Keycode.SDLK_o => Key.O,
            SDL.SDL_Keycode.SDLK_p => Key.P,
            SDL.SDL_Keycode.SDLK_q => Key.Q,
            SDL.SDL_Keycode.SDLK_r => Key.R,
            SDL.SDL_Keycode.SDLK_s => Key.S,
            SDL.SDL_Keycode.SDLK_t => Key.T,
            SDL.SDL_Keycode.SDLK_u => Key.U,
            SDL.SDL_Keycode.SDLK_v => Key.V,
            SDL.SDL_Keycode.SDLK_w => Key.W,
            SDL.SDL_Keycode.SDLK_x => Key.X,
            SDL.SDL_Keycode.SDLK_y => Key.Y,
            SDL.SDL_Keycode.SDLK_z => Key.Z,
            SDL.SDL_Keycode.SDLK_0 => Key.Num0,
            SDL.SDL_Keycode.SDLK_1 => Key.Num1,
            SDL.SDL_Keycode.SDLK_2 => Key.Num2,
            SDL.SDL_Keycode.SDLK_3 => Key.Num3,
            SDL.SDL_Keycode.SDLK_4 => Key.Num4,
            SDL.SDL_Keycode.SDLK_5 => Key.Num5,
            SDL.SDL_Keycode.SDLK_6 => Key.Num6,
            SDL.SDL_Keycode.SDLK_7 => Key.Num7,
            SDL.SDL_Keycode.SDLK_8 => Key.Num8,
            SDL.SDL_Keycode.SDLK_9 => Key.Num9,
            SDL.SDL_Keycode.SDLK_ESCAPE => Key.Escape,
            SDL.SDL_Keycode.SDLK_SPACE => Key.Space,
            SDL.SDL_Keycode.SDLK_RETURN => Key.Enter,
            SDL.SDL_Keycode.SDLK_LSHIFT => Key.Shift,
            SDL.SDL_Keycode.SDLK_LCTRL => Key.Control,
            SDL.SDL_Keycode.SDLK_LALT => Key.Alt,
            SDL.SDL_Keycode.SDLK_LEFT => Key.LeftArrow,
            SDL.SDL_Keycode.SDLK_RIGHT => Key.RightArrow,
            SDL.SDL_Keycode.SDLK_UP => Key.UpArrow,
            SDL.SDL_Keycode.SDLK_DOWN => Key.DownArrow,
            SDL.SDL_Keycode.SDLK_PLUS => Key.Plus,
            SDL.SDL_Keycode.SDLK_MINUS => Key.Minus,
            SDL.SDL_Keycode.SDLK_EQUALS => Key.Equals,
            SDL.SDL_Keycode.SDLK_BACKSPACE => Key.Backspace,
            SDL.SDL_Keycode.SDLK_TAB => Key.Tab,
            SDL.SDL_Keycode.SDLK_CAPSLOCK => Key.CapsLock,
            SDL.SDL_Keycode.SDLK_DELETE => Key.Delete,
            SDL.SDL_Keycode.SDLK_F1 => Key.F1,
            SDL.SDL_Keycode.SDLK_F2 => Key.F2,
            SDL.SDL_Keycode.SDLK_F3 => Key.F3,
            SDL.SDL_Keycode.SDLK_F4 => Key.F4,
            SDL.SDL_Keycode.SDLK_F5 => Key.F5,
            SDL.SDL_Keycode.SDLK_F6 => Key.F6,
            SDL.SDL_Keycode.SDLK_F7 => Key.F7,
            SDL.SDL_Keycode.SDLK_F8 => Key.F8,
            SDL.SDL_Keycode.SDLK_F9 => Key.F9,
            SDL.SDL_Keycode.SDLK_F10 => Key.F10,
            SDL.SDL_Keycode.SDLK_F11 => Key.F11,
            SDL.SDL_Keycode.SDLK_F12 => Key.F12
            };
        }
        
    }

}