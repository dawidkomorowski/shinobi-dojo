using Geisha.Engine.Core.SceneModel;
using ShinobiDojo.Character;

namespace ShinobiDojo
{
    internal sealed class MainSceneBehaviorFactory : ISceneBehaviorFactory
    {
        private const string SceneBehaviorName = "Main";
        public string BehaviorName => SceneBehaviorName;

        public SceneBehavior Create(Scene scene) => new MainSceneBehavior(scene);

        private sealed class MainSceneBehavior : SceneBehavior
        {
            public MainSceneBehavior(Scene scene) : base(scene)
            {
            }

            public override string Name => SceneBehaviorName;

            protected override void OnLoaded()
            {
                CameraEntityFactory.CreateCamera(Scene);
                GroundEntityFactory.CreateGround(Scene);
                BorderEntityFactory.CreateLeftBorder(Scene);
                BorderEntityFactory.CreateRightBorder(Scene);
                CharacterEntityFactory.CreatePlayerCharacter(Scene, 300);
                CharacterEntityFactory.CreateAiCharacter(Scene, -300);
            }
        }
    }
}