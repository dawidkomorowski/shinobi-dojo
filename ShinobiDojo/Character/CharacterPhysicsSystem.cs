using System.Linq;
using Geisha.Engine.Core;
using Geisha.Engine.Core.Components;
using Geisha.Engine.Core.SceneModel;
using Geisha.Engine.Core.Systems;

namespace ShinobiDojo.Character
{
    internal sealed class CharacterPhysicsSystem : ICustomSystem
    {
        public string Name => "ShinobiDojo.CharacterPhysicsSystem";

        public void ProcessFixedUpdate(Scene scene)
        {
            var entities = scene.RootEntities.Where(e =>
                e.HasComponent<Transform2DComponent>() && e.HasComponent<CharacterPhysicsComponent>()).ToList();

            foreach (var entity in entities)
            {
                var transform = entity.GetComponent<Transform2DComponent>();
                var characterPhysics = entity.GetComponent<CharacterPhysicsComponent>();

                transform.Translation += characterPhysics.Velocity * GameTime.FixedDeltaTime.TotalSeconds;
            }
        }

        public void ProcessUpdate(Scene scene, GameTime gameTime)
        {
        }
    }
}