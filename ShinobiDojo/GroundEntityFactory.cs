using Geisha.Common.Math;
using Geisha.Engine.Core.Components;
using Geisha.Engine.Core.SceneModel;
using Geisha.Engine.Physics.Components;
using Geisha.Engine.Rendering.Components;

namespace ShinobiDojo
{
    internal sealed class GroundEntityFactory
    {
        public Entity CreateGround()
        {
            var groundEntity = new Entity {Name = EntityName.Ground};

            groundEntity.AddComponent(new Transform2DComponent
            {
                Translation = new Vector2(0, -300),
                Rotation = 0,
                Scale = Vector2.One
            });

            var dimension = new Vector2(1280, 50);
            groundEntity.AddComponent(new RectangleRendererComponent
            {
                Color = Color.FromArgb(255, 0, 0, 255),
                Dimension = dimension,
                FillInterior = true
            });
            groundEntity.AddComponent(new RectangleColliderComponent
            {
                Dimension = dimension
            });

            return groundEntity;
        }
    }
}