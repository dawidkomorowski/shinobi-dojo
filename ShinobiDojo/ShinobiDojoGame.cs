using System.Reflection;
using Geisha.Engine;

namespace ShinobiDojo
{
    internal sealed class ShinobiDojoGame : IGame
    {
        public void RegisterComponents(IComponentsRegistry componentsRegistry)
        {
            componentsRegistry.RegisterSceneConstructionScript<SetUpLevelSceneConstructionScript>();
        }

        public string WindowTitle => $"ShinobiDojo {Assembly.GetAssembly(typeof(ShinobiDojoGame))?.GetName().Version} ({EngineInformation})";
        private static string EngineInformation => $"Geisha Engine {Assembly.GetAssembly(typeof(IGame))?.GetName().Version}";
    }
}