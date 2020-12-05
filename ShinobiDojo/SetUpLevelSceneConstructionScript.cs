using Geisha.Engine.Core.SceneModel;

namespace ShinobiDojo
{
    internal sealed class SetUpLevelSceneConstructionScript : ISceneConstructionScript
    {
        public void Execute(Scene scene)
        {
            var cameraEntityFactory = new CameraEntityFactory();
            var cameraEntity = cameraEntityFactory.CreateCamera();
            scene.AddEntity(cameraEntity);

            var groundEntityFactory = new GroundEntityFactory();
            var groundEntity = groundEntityFactory.CreateGround();
            scene.AddEntity(groundEntity);
        }

        public string Name => "SetUpLevel";
    }
}