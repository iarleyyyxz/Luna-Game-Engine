using System.Numerics;
using Frent;
using Frent.Components;
using Frent.Core;
using Luna.Ecs;

namespace Luna.g2d
{
    public struct Camera2D
    {
        public float Zoom;
        public Vector2 ProjectionSize;

        public Matrix4x4 View;
        public Matrix4x4 Projection;
        public Matrix4x4 InverseView;
        public Matrix4x4 InverseProjection;
        public Transform2D Transform;
        public Tag IsMainCamera;
    }
}