using Frent;
using Luna.Ecs;
using Luna.g2d.Providers;

namespace Luna.g2d.Interfaces
{
    public interface IScene
    {
        void Start();

        void Update(float deltaTime);

        void Stop();

        World GetWorld();

        Camera2D GetCamera();
        
        SystemProvider GetSystems();

    }
}