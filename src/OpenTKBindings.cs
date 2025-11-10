using System;
using System.Runtime.InteropServices;
using OpenTK;
using SDL2;

public class OpenTKBindings : IBindingsContext
{
    public IntPtr GetProcAddress(string procName)
    {
        return SDL.SDL_GL_GetProcAddress(procName);
    }
}
