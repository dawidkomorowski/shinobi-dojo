using Geisha.Common.Math;
using Geisha.Engine.Core.Components;
using Geisha.Engine.Core.SceneModel;
using Geisha.Engine.Physics.Components;
using Geisha.Engine.Rendering.Components;

namespace ShinobiDojo
{
    internal sealed class BorderEntityFactory
    {
        public Entity CreateLeftBorder()
        {
            return CreateBorder(EntityName.LeftBorder, -640);
        }

        public Entity CreateRightBorder()
        {
            return CreateBorder(EntityName.RightBorder, 640);
        }

        private static Entity CreateBorder(string name, double xPos)
        {
            var borderEntity = new Entity {Name = name};

            borderEntity.AddComponent(new Transform2DComponent
            {
                Translation = new Vector2(xPos, 0),
                Rotation = 0,
                Scale = Vector2.One
            });

            var dimension = new Vector2(50, 10000);
            borderEntity.AddComponent(new RectangleRendererComponent
            {
                Color = Color.FromArgb(255, 127, 127, 255),
                Dimension = dimension,
                FillInterior = true
            });
            borderEntity.AddComponent(new RectangleColliderComponent
            {
                Dimension = dimension
            });

            return borderEntity;
        }
    }
}