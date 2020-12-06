using System.Reflection;
using Geisha.Engine;
using ShinobiDojo.Character;

namespace ShinobiDojo
{
    internal sealed class ShinobiDojoGame : IGame
    {
        public void RegisterComponents(IComponentsRegistry componentsRegistry)
        {
            // Character
            componentsRegistry.RegisterSystem<CharacterPhysicsSystem>();

            componentsRegistry.RegisterSceneConstructionScript<SetUpLevelSceneConstructionScript>();
        }

        public string WindowTitle =>
            $"ShinobiDojo {Assembly.GetAssembly(typeof(ShinobiDojoGame))?.GetName().Version} ({EngineInformation})";

        private static string EngineInformation =>
            $"Geisha Engine {Assembly.GetAssembly(typeof(IGame))?.GetName().Version}";
    }
}