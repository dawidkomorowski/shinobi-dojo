using System.Linq;
using Geisha.Common.Math;
using Geisha.Engine.Core;
using Geisha.Engine.Core.Components;
using Geisha.Engine.Core.SceneModel;
using Geisha.Engine.Core.Systems;
using Geisha.Engine.Physics.Components;

namespace ShinobiDojo.Character
{
    internal sealed class CharacterPhysicsSystem : ICustomSystem
    {
        private const double GravitationalAcceleration = 2000;

        public string Name => "ShinobiDojo.CharacterPhysicsSystem";

        public void ProcessFixedUpdate(Scene scene)
        {
            var entities = scene.RootEntities.Where(e =>
                e.HasComponent<Transform2DComponent>() && e.HasComponent<CharacterPhysicsComponent>()).ToList();

            foreach (var entity in entities)
            {
                var transform = entity.GetComponent<Transform2DComponent>();
                var collider = entity.GetComponent<RectangleColliderComponent>();
                var characterPhysics = entity.GetComponent<CharacterPhysicsComponent>();

                ApplyGravity(characterPhysics);
                var translationDelta = ComputeTranslationDelta(characterPhysics);
                translationDelta = ResolveCollisionWithGround(collider, characterPhysics, translationDelta);
                translationDelta = ResolveCollisionWithBorders(collider, characterPhysics, translationDelta);
                UpdatePosition(transform, translationDelta);
            }
        }

        public void ProcessUpdate(Scene scene, GameTime gameTime)
        {
        }

        private static void ApplyGravity(CharacterPhysicsComponent characterPhysics)
        {
            if (characterPhysics.StandingOnTheGround == false)
            {
                characterPhysics.Velocity += new Vector2(0, -GravitationalAcceleration * GameTime.FixedDeltaTime.TotalSeconds);
            }
        }

        private static Vector2 ComputeTranslationDelta(CharacterPhysicsComponent characterPhysics)
        {
            return characterPhysics.Velocity * GameTime.FixedDeltaTime.TotalSeconds;
        }

        private static Vector2 ResolveCollisionWithGround(RectangleColliderComponent collider, CharacterPhysicsComponent characterPhysics,
            Vector2 translationDelta)
        {
            if (characterPhysics.StandingOnTheGround)
            {
                if (CharacterIsCollidingWithEntity(collider, EntityName.Ground) == false)
                {
                    characterPhysics.StandingOnTheGround = false;
                }

                return translationDelta;
            }

            if (CharacterIsCollidingWithEntity(collider, EntityName.Ground))
            {
                characterPhysics.StandingOnTheGround = true;
                characterPhysics.Velocity = characterPhysics.Velocity.WithY(0);
                return translationDelta.WithY(0);
            }

            return translationDelta;
        }

        private static Vector2 ResolveCollisionWithBorders(RectangleColliderComponent collider, CharacterPhysicsComponent characterPhysics,
            Vector2 translationDelta)
        {
            if (CharacterIsCollidingWithEntity(collider, EntityName.LeftBorder) && translationDelta.X < 0)
            {
                characterPhysics.Velocity = characterPhysics.Velocity.WithX(0);
                return translationDelta.WithX(0);
            }

            if (CharacterIsCollidingWithEntity(collider, EntityName.RightBorder) && translationDelta.X > 0)
            {
                characterPhysics.Velocity = characterPhysics.Velocity.WithX(0);
                return translationDelta.WithX(0);
            }

            return translationDelta;
        }

        private static void UpdatePosition(Transform2DComponent transform, Vector2 translationDelta)
        {
            transform.Translation += translationDelta;
        }

        private static bool CharacterIsCollidingWithEntity(RectangleColliderComponent collider, string entityName)
        {
            return collider.IsColliding && collider.CollidingEntities.Any(e => e.Name == entityName);
        }
    }
}