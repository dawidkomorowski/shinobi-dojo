using System.Linq;
using Geisha.Engine.Core;
using Geisha.Engine.Core.SceneModel;
using Geisha.Engine.Core.Systems;

namespace ShinobiDojo.Character
{
    internal sealed class CharacterControllerSystem : ICustomSystem
    {
        public string Name => "ShinobiDojo.CharacterControllerSystem";

        public void ProcessFixedUpdate(Scene scene)
        {
            var entities = scene.RootEntities.Where(e =>
                e.HasComponent<CharacterPhysicsComponent>() && e.HasComponent<CharacterControllerComponent>()).ToList();

            foreach (var entity in entities)
            {
                var characterPhysics = entity.GetComponent<CharacterPhysicsComponent>();
                var characterController = entity.GetComponent<CharacterControllerComponent>();

                characterController.Process(characterPhysics);
            }
        }

        public void ProcessUpdate(Scene scene, GameTime gameTime)
        {
        }
    }
}