using System;
using SDL2;
using Luna.IO;
using Luna.Editor;
using Luna.Util;
using Luna.Preferences;
using OpenTK.Graphics.OpenGL4;

public class Window
{
    private IntPtr _window;
    private IntPtr _renderer;
    public bool IsRunning { get; private set; }

    public int Width { get; set; }
    public int Height { get; set; }
    public string Title { get; set; }

    bool isFullscreen = true;

    public Window(string title, int width, int height)
    {
        Width = width;
        Height = height;
        Title = title;

        if (SDL.SDL_Init(SDL.SDL_INIT_VIDEO) < 0)
            throw new Exception(SDL.SDL_GetError());

        SDL.SDL_GL_SetAttribute(SDL.SDL_GLattr.SDL_GL_CONTEXT_MAJOR_VERSION, 3);
        SDL.SDL_GL_SetAttribute(SDL.SDL_GLattr.SDL_GL_CONTEXT_MINOR_VERSION, 3);
        SDL.SDL_GL_SetAttribute(SDL.SDL_GLattr.SDL_GL_CONTEXT_PROFILE_MASK, (int)SDL.SDL_GLprofile.SDL_GL_CONTEXT_PROFILE_CORE);
        SDL.SDL_GL_SetAttribute(SDL.SDL_GLattr.SDL_GL_DOUBLEBUFFER, 1);
        SDL.SDL_GL_SetAttribute(SDL.SDL_GLattr.SDL_GL_DEPTH_SIZE, 24);

        _window = SDL.SDL_CreateWindow(title, SDL.SDL_WINDOWPOS_CENTERED, SDL.SDL_WINDOWPOS_CENTERED, width, height, SDL.SDL_WindowFlags.SDL_WINDOW_OPENGL | SDL.SDL_WindowFlags.SDL_WINDOW_SHOWN |
        SDL.SDL_WindowFlags.SDL_WINDOW_RESIZABLE);

        IntPtr glContext = SDL.SDL_GL_CreateContext(_window);
        if (glContext == IntPtr.Zero)
            throw new Exception("GL Context Error: " + SDL.SDL_GetError());

        SDL.SDL_GL_MakeCurrent(_window, glContext);

        // 6) VSync (0 = off, 1 = on, -1 = adaptive)
        SDL.SDL_GL_SetSwapInterval(1);


        OpenTK.Graphics.OpenGL4.GL.LoadBindings(new OpenTKBindings());
        
        SetWindowIcon("assets/icons/LunaIcon.bmp");
        _renderer = SDL.SDL_CreateRenderer(_window, -1, SDL.SDL_RendererFlags.SDL_RENDERER_ACCELERATED |
        SDL.SDL_RendererFlags.SDL_RENDERER_PRESENTVSYNC);

        
        //SDL.SDL_SetWindowFullscreen(_window, (uint)SDL.SDL_WindowFlags.SDL_WINDOW_FULLSCREEN_DESKTOP);
        SDL.SDL_SetWindowFullscreen(_window, (uint)SDL.SDL_WindowFlags.SDL_WINDOW_FULLSCREEN_DESKTOP);

        SDL.SDL_StartTextInput();

        Font.Init(_renderer, "assets/SORA-REGULAR.ttf", 16);

        IntPtr texOn = LoadTexture(_renderer, "assets/icons/CheckBoxIconLuna.png");
        IntPtr texOff = LoadTexture(_renderer, "assets/icons/CheckBoxIconLuna2.png");

        IsRunning = true;
    }

    public void Run()
    {
        while (IsRunning)
        {
            ProcessEvents();

            GL.ClearColor(1f, 0f, 0f, 1f);
            GL.Clear(ClearBufferMask.ColorBufferBit);

            SDL.SDL_GL_SwapWindow(_window);


            SDL.SDL_SetRenderDrawColor(_renderer, 0, 1, 2, 255);
            SDL.SDL_RenderClear(_renderer);

            Time.Update();

            
            SDL.SDL_RenderPresent(_renderer);
        }

        Quit();
    }

    private void ProcessEvents()
    {
        while (SDL.SDL_PollEvent(out SDL.SDL_Event e) == 1)
        {
            Keyboard.ProcessEvent(e);
            Mouse.ProcessEvent(e);

            if (e.type == SDL.SDL_EventType.SDL_KEYDOWN && e.key.keysym.sym == SDL.SDL_Keycode.SDLK_F11)
            {
                ToggleFullscreen();
            }

            if (e.type == SDL.SDL_EventType.SDL_WINDOWEVENT)
            {
                if (e.window.windowEvent == SDL.SDL_WindowEventID.SDL_WINDOWEVENT_SIZE_CHANGED)
                {
                    int w, h;
                    SDL.SDL_GetWindowSize(_window, out w, out h);
                    Width = w;
                    Height = h;
                    UIManager.OnResize(Width, Height);
                }
            }

            if (e.type == SDL.SDL_EventType.SDL_QUIT)
                    IsRunning = false;
        }

        void ToggleFullscreen()
        {
            if (!isFullscreen)
            {
                SDL.SDL_SetWindowFullscreen(_window, (uint)SDL.SDL_WindowFlags.SDL_WINDOW_FULLSCREEN_DESKTOP);
                isFullscreen = true;
            }
            else
            {
                SDL.SDL_SetWindowFullscreen(_window, 0);
                isFullscreen = false;
            }

        }
    }

    public void Quit()
    {
        SDL.SDL_DestroyRenderer(_renderer);
        SDL.SDL_DestroyWindow(_window);
        SDL.SDL_Quit();
        Font.Quit();
    }

    public static IntPtr LoadTexture(IntPtr renderer, string filePath)
    {
        IntPtr surface = SDL_image.IMG_Load(filePath);

        if (surface == IntPtr.Zero)
            throw new Exception("Error on load texture: " + SDL.SDL_GetError());

        IntPtr texture = SDL.SDL_CreateTextureFromSurface(renderer, surface);
        SDL.SDL_FreeSurface(surface);

        if (texture == IntPtr.Zero)
            throw new Exception("Error on create texture: " + SDL.SDL_GetError());

        return texture;
    }

    private void SetWindowIcon(string path)
    {
        IntPtr icon = SDL.SDL_LoadBMP(path);
        if (icon == IntPtr.Zero)
        {
            Console.WriteLine("❌ Erro ao carregar icone: " + SDL.SDL_GetError());
            return;
        }

        SDL.SDL_SetWindowIcon(_window, icon);
        SDL.SDL_FreeSurface(icon);
    }
}
