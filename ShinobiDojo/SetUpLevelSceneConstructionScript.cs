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

            var borderEntityFactory = new BorderEntityFactory();
            var leftBorderEntity = borderEntityFactory.CreateLeftBorder();
            scene.AddEntity(leftBorderEntity);
            var rightBorderEntity = borderEntityFactory.CreateRightBorder();
            scene.AddEntity(rightBorderEntity);

            var characterEntityFactory = new CharacterEntityFactory();
            var playerCharacterEntity = characterEntityFactory.CreatePlayerCharacter(300);
            scene.AddEntity(playerCharacterEntity);
            var aiCharacterEntity = characterEntityFactory.CreateAiCharacter(-300);
            scene.AddEntity(aiCharacterEntity);
        }

        public string Name => "SetUpLevel";
    }
}