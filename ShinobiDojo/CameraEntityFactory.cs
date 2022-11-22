using Geisha.Engine.Core.Components;
using Geisha.Engine.Core.Math;
using Geisha.Engine.Core.SceneModel;
using Geisha.Engine.Rendering.Components;

namespace ShinobiDojo
{
    internal static class CameraEntityFactory
    {
        public static void CreateCamera(Scene scene)
        {
            var cameraEntity = scene.CreateEntity();

            cameraEntity.CreateComponent<Transform2DComponent>();
            var camera = cameraEntity.CreateComponent<CameraComponent>();
            camera.ViewRectangle = new Vector2(1280, 720);
        }
    }
}