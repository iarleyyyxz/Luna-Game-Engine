using Frent;

namespace Luna.g2d.Interfaces
{
    public interface IBehaviour
    {
        virtual void Init(World world) {}

        virtual void Update(float dt) {}

        virtual void Render() {}

        void Dispose();
    }
}