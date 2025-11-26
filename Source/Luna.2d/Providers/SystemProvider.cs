using Frent;
using Luna.g2d.Interfaces;
using System.Collections.Generic;

namespace Luna.g2d.Providers
{
    public class SystemProvider
    {
        private readonly List<IBehaviour> systems = new();

        public void AddSystem(IBehaviour system, World world)
        {
            system.Init(world);
            systems.Add(system);
        }

        public void Update(float deltaTime)
        {
            foreach (var system in systems)
                system.Update(deltaTime);
        }

        public void Render()
        {
            foreach (var system in systems)
                system.Render();
        }

        public void Dispose()
        {
            foreach (var system in systems)
                system.Dispose();

            systems.Clear();
        
        }
    }
}