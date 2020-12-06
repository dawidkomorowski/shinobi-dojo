using Geisha.Engine.Core.SceneModel;
using ShinobiDojo.Character;

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

            var characterEntityFactory = new CharacterEntityFactory();
            var characterEntity = characterEntityFactory.CreatePlayerCharacter();
            scene.AddEntity(characterEntity);
        }

        public string Name => "SetUpLevel";
    }
}