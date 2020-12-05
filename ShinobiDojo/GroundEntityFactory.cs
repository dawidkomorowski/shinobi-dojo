using Geisha.Common.Math;
using Geisha.Engine.Core.Components;
using Geisha.Engine.Core.SceneModel;
using Geisha.Engine.Rendering.Components;

namespace ShinobiDojo
{
    internal sealed class GroundEntityFactory
    {
        public Entity CreateGround()
        {
            var groundEntity = new Entity();

            groundEntity.AddComponent(new Transform2DComponent
            {
                Translation = new Vector2(0, -300),
                Rotation = 0,
                Scale = Vector2.One
            });
            groundEntity.AddComponent(new RectangleRendererComponent
            {
                Color = Color.FromArgb(255, 0, 0, 255),
                Dimension = new Vector2(1280, 50),
                FillInterior = true
            });

            return groundEntity;
        }
    }
}