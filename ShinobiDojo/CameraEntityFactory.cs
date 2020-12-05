using Geisha.Common.Math;
using Geisha.Engine.Core.Components;
using Geisha.Engine.Core.SceneModel;
using Geisha.Engine.Rendering.Components;

namespace ShinobiDojo
{
    internal sealed class CameraEntityFactory
    {
        public Entity CreateCamera()
        {
            var cameraEntity = new Entity();

            cameraEntity.AddComponent(Transform2DComponent.CreateDefault());
            cameraEntity.AddComponent(new CameraComponent
            {
                ViewRectangle = new Vector2(1280, 720)
            });

            return cameraEntity;
        }
    }
}