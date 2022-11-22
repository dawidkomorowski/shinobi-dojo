using Geisha.Engine.Core.Components;
using Geisha.Engine.Core.Math;
using Geisha.Engine.Core.SceneModel;
using Geisha.Engine.Physics.Components;
using Geisha.Engine.Rendering.Components;

namespace ShinobiDojo
{
    internal static class BorderEntityFactory
    {
        public static void CreateLeftBorder(Scene scene)
        {
            CreateBorder(scene, EntityName.LeftBorder, -640);
        }

        public static void CreateRightBorder(Scene scene)
        {
            CreateBorder(scene, EntityName.RightBorder, 640);
        }

        private static void CreateBorder(Scene scene, string name, double xPos)
        {
            var borderEntity = scene.CreateEntity();
            borderEntity.Name = name;

            var transform = borderEntity.CreateComponent<Transform2DComponent>();
            transform.Translation = new Vector2(xPos, 0);
            transform.Rotation = 0;
            transform.Scale = Vector2.One;

            var dimensions = new Vector2(50, 10000);
            var rectangleRenderer = borderEntity.CreateComponent<RectangleRendererComponent>();
            rectangleRenderer.Color = Color.FromArgb(255, 127, 127, 255);
            rectangleRenderer.Dimension = dimensions;
            rectangleRenderer.FillInterior = true;

            var colliderComponent = borderEntity.CreateComponent<RectangleColliderComponent>();
            colliderComponent.Dimensions = dimensions;
        }
    }
}