using Geisha.Engine.Core.Math;
using Geisha.Engine.Core.Components;
using Geisha.Engine.Core.SceneModel;
using Geisha.Engine.Physics.Components;
using Geisha.Engine.Rendering.Components;

namespace ShinobiDojo
{
    internal static class GroundEntityFactory
    {
        public static void CreateGround(Scene scene)
        {
            var groundEntity = scene.CreateEntity();
            groundEntity.Name = EntityName.Ground;

            var transform = groundEntity.CreateComponent<Transform2DComponent>();
            transform.Translation = new Vector2(0, -300);
            transform.Rotation = 0;
            transform.Scale = Vector2.One;

            var dimensions = new Vector2(1280, 50);
            var rectangleRenderer = groundEntity.CreateComponent<RectangleRendererComponent>();
            rectangleRenderer.Color = Color.FromArgb(255, 0, 0, 255);
            rectangleRenderer.Dimension = dimensions;
            rectangleRenderer.FillInterior = true;

            var rectangleCollider = groundEntity.CreateComponent<RectangleColliderComponent>();
            rectangleCollider.Dimensions = dimensions;
        }
    }
}