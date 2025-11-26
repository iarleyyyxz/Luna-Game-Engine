using System.Numerics;
using Frent;
using Frent.Core;
using Luna.g2d;
using Luna.g2d.Interfaces;

namespace Luna.Ecs
{
    public class Camera2DSystem : IBehaviour
    {
        public World world;

        public void Init(World world)
        {
            this.world = world;
        }

        public void Update(float dt)
        {
            foreach((Ref<Transform2D> transform, Ref<Camera2D> camera) in world.Query<Transform2D, Camera2D>()
                .Enumerate<Transform2D, Camera2D>())
            {
               // camera.Value.View = AdjustProjection(transform.Value, camera);
            }
        }

        public void Dispose()
        {
        
        }
    }
}