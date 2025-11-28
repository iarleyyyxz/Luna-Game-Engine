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
                camera.Value.View = CreateProjection(transform.Value.Position, camera);
             //   camera.Value.Projection 
            }
        }

        public Matrix4x4 CreateProjection(Vector2 pos, Ref<Camera2D> camera)
        {
            float w = camera.Value.ProjectionSize.X * camera.Value.Zoom;
            float h = camera.Value.ProjectionSize.Y * camera.Value.Zoom;

            return Matrix4x4.CreateOrthographicOffCenter(
                0, w,
                h, 0,
                -1f, 100f
            );
        }


        public void Dispose()
        {
        
        }
    }
}