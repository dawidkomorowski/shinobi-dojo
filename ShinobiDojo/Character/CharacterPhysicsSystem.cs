using System.Linq;
using Geisha.Common.Math;
using Geisha.Engine.Core;
using Geisha.Engine.Core.Components;
using Geisha.Engine.Core.SceneModel;
using Geisha.Engine.Core.Systems;

namespace ShinobiDojo.Character
{
    internal sealed class CharacterPhysicsSystem : ICustomSystem
    {
        private const double GravitationalAcceleration = 10;

        public string Name => "ShinobiDojo.CharacterPhysicsSystem";

        public void ProcessFixedUpdate(Scene scene)
        {
            var entities = scene.RootEntities.Where(e =>
                e.HasComponent<Transform2DComponent>() && e.HasComponent<CharacterPhysicsComponent>()).ToList();

            foreach (var entity in entities)
            {
                var transform = entity.GetComponent<Transform2DComponent>();
                var characterPhysics = entity.GetComponent<CharacterPhysicsComponent>();

                ApplyGravity(characterPhysics);
                IntegratePosition(transform, characterPhysics);
            }
        }

        public void ProcessUpdate(Scene scene, GameTime gameTime)
        {
        }

        private static void ApplyGravity(CharacterPhysicsComponent characterPhysics)
        {
            characterPhysics.Velocity += new Vector2(0, -GravitationalAcceleration);
        }

        private static void IntegratePosition(Transform2DComponent transform, CharacterPhysicsComponent characterPhysics)
        {
            transform.Translation += characterPhysics.Velocity * GameTime.FixedDeltaTime.TotalSeconds;
        }
    }
}