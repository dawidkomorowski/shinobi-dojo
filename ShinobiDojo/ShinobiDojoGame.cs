using System.Reflection;
using Geisha.Engine;
using ShinobiDojo.Character;

namespace ShinobiDojo
{
    internal sealed class ShinobiDojoGame : Game
    {
        public override string WindowTitle =>
            $"Shinobi Dojo {Assembly.GetAssembly(typeof(ShinobiDojoGame))?.GetName().Version?.ToString(2)} ({EngineInformation})";

        private static string EngineInformation =>
            $"Geisha Engine {Assembly.GetAssembly(typeof(Game))?.GetName().Version?.ToString(3)}";

        public override void RegisterComponents(IComponentsRegistry componentsRegistry)
        {
            // Character
            componentsRegistry.RegisterSystem<CharacterControllerSystem>();
            componentsRegistry.RegisterComponentFactory<CharacterControllerComponentFactory>();
            componentsRegistry.RegisterComponentFactory<PlayerCharacterControllerComponentFactory>();
            componentsRegistry.RegisterComponentFactory<AiCharacterControllerComponentFactory>();

            componentsRegistry.RegisterSystem<CharacterPhysicsSystem>();
            componentsRegistry.RegisterComponentFactory<CharacterPhysicsComponentFactory>();

            componentsRegistry.RegisterSceneBehaviorFactory<MainSceneBehaviorFactory>();
        }
    }
}