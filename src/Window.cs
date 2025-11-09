using System;
using SDL2;
using Luna.IO;
using Luna.Editor;
using Luna.Util;
using Luna.Preferences;

public class Window
{
    private IntPtr _window;
    private IntPtr _renderer;
    public bool IsRunning { get; private set; }

    private UIInputLabel uIInputLabel;

    public int Width { get; set; }
    public int Height { get; set; }
    public string Title { get; set; }

    private UIPanel panel;

    bool isFullscreen = true;

    public Window(string title, int width, int height)
    {
        Width = width;
        Height = height;
        Title = title;

        if (SDL.SDL_Init(SDL.SDL_INIT_VIDEO) < 0)
            throw new Exception(SDL.SDL_GetError());

        _window = SDL.SDL_CreateWindow(title, SDL.SDL_WINDOWPOS_CENTERED, SDL.SDL_WINDOWPOS_CENTERED, width, height, SDL.SDL_WindowFlags.SDL_WINDOW_SHOWN |
        SDL.SDL_WindowFlags.SDL_WINDOW_RESIZABLE);
        _renderer = SDL.SDL_CreateRenderer(_window, -1, SDL.SDL_RendererFlags.SDL_RENDERER_ACCELERATED | SDL.SDL_RendererFlags.SDL_RENDERER_PRESENTVSYNC);

        //SDL.SDL_SetWindowFullscreen(_window, (uint)SDL.SDL_WindowFlags.SDL_WINDOW_FULLSCREEN_DESKTOP);
        SDL.SDL_SetWindowFullscreen(_window, (uint)SDL.SDL_WindowFlags.SDL_WINDOW_FULLSCREEN_DESKTOP);

        SDL.SDL_StartTextInput();

        Font.Init(_renderer, "assets/SORA-REGULAR.ttf", 16);

        // Supondo que você já tenha texturas para checkbox
        IntPtr texOn = LoadTexture(_renderer, "assets/icons/CheckBoxIconLuna.png");
        IntPtr texOff = LoadTexture(_renderer, "assets/icons/CheckBoxIconLuna2.png");

        panel = new UIPanel
        {
            X = 50,
            Y = 50,
            Width = 300,
            Height = 200,
            Title = "Meu Painel Genérico"
        };

        // Criar outra UIElement qualquer (ex.: um botão ou label fictício)


        // Criar grupo de checkboxes
        var group = new UICheckboxGroup { Layout = Orientation.Vertical, Spacing = 10 };
        group.Checkboxes.Add(new UICheckBox(texOn, texOff, "Opção 1"));
        group.Checkboxes.Add(new UICheckBox(texOn, texOff, "Opção 2"));

        // Adicionar grupo ao painel
        
        panel.AddChild(group);
    





        IsRunning = true;
    }

    public void Run()
    {
        while (IsRunning)
        {
            ProcessEvents();

            SDL.SDL_SetRenderDrawColor(_renderer, 0, 1, 2, 255);
            SDL.SDL_RenderClear(_renderer);

            // ✅ Atualiza UI
            Time.Update();
            panel.Update();
            panel.Draw(_renderer);

            
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
            throw new Exception("Erro ao carregar imagem: " + SDL.SDL_GetError());

        IntPtr texture = SDL.SDL_CreateTextureFromSurface(renderer, surface);
        SDL.SDL_FreeSurface(surface);

        if (texture == IntPtr.Zero)
            throw new Exception("Erro ao criar textura: " + SDL.SDL_GetError());

        return texture;
    }
}
