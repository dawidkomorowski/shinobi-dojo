using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Geisha.Engine.Core.Math;
using Geisha.Engine.Core;
using Geisha.Engine.Core.Components;
using Geisha.Engine.Core.SceneModel;
using Geisha.Engine.Core.Systems;
using Geisha.Engine.Physics.Components;

namespace ShinobiDojo.Character
{
    internal sealed class CharacterPhysicsSystem : ICustomSystem
    {
        private const double GravitationalAcceleration = 5000;
        private Scene? _scene;

        #region ICustomSystem implementation

        public string Name => "ShinobiDojo.CharacterPhysicsSystem";

        public void ProcessFixedUpdate()
        {
            // TODO This is naive implementation to quickly migrate code from Geisha Engine 0.5 to Geisha Engine 0.8
            if (_scene is not null)
            {
                InternalProcessFixedUpdate(_scene);
            }
        }

        public void ProcessUpdate(GameTime gameTime)
        {
        }

        public void OnEntityCreated(Entity entity)
        {
            _scene = entity.Scene;
        }

        public void OnEntityRemoved(Entity entity)
        {
        }

        public void OnEntityParentChanged(Entity entity, Entity? oldParent, Entity? newParent)
        {
        }

        public void OnComponentCreated(Component component)
        {
        }

        public void OnComponentRemoved(Component component)
        {
        }

        #endregion

        private void InternalProcessFixedUpdate(Scene scene)
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
                translationDelta = ResolveCollisionWithGround(transform, collider, characterPhysics, translationDelta);
                translationDelta = ResolveCollisionWithBorders(collider, characterPhysics, translationDelta);
                translationDelta = ResolveCollisionWithOtherCharacter(transform, collider, characterPhysics, translationDelta);
                UpdatePosition(transform, translationDelta);
            }
        }

        private static void ApplyGravity(CharacterPhysicsComponent characterPhysics)
        {
            if (characterPhysics.StandingOnTheGround == false)
            {
                characterPhysics.Velocity += new Vector2(0, -GravitationalAcceleration) * GameTime.FixedDeltaTime.TotalSeconds;
            }
        }

        private static Vector2 ComputeTranslationDelta(CharacterPhysicsComponent characterPhysics)
        {
            return characterPhysics.Velocity * GameTime.FixedDeltaTime.TotalSeconds;
        }

        private static Vector2 ResolveCollisionWithGround(Transform2DComponent transform, RectangleColliderComponent collider,
            CharacterPhysicsComponent characterPhysics,
            Vector2 translationDelta)
        {
            if (characterPhysics.StandingOnTheGround)
            {
                if (CharacterIsCollidingWithGround(collider, out _) == false)
                {
                    characterPhysics.StandingOnTheGround = false;
                }

                return translationDelta;
            }

            if (CharacterIsCollidingWithGround(collider, out var groundEntity))
            {
                characterPhysics.StandingOnTheGround = true;
                characterPhysics.Velocity = characterPhysics.Velocity.WithY(0);

                var penetrationFixingDistance = ComputeCharacterToGroundPenetrationFixingDistance(transform, collider, groundEntity);

                return translationDelta.WithY(penetrationFixingDistance);
            }

            return translationDelta;
        }

        private static double ComputeCharacterToGroundPenetrationFixingDistance(Transform2DComponent transform, RectangleColliderComponent collider,
            Entity groundEntity)
        {
            var groundTransform = groundEntity.GetComponent<Transform2DComponent>();
            var groundCollider = groundEntity.GetComponent<RectangleColliderComponent>();

            var centersDistance = transform.Translation.Y - groundTransform.Translation.Y;
            var minimalNotCollidingCentersDistance = collider.Dimensions.Y * 0.5 + groundCollider.Dimensions.Y * 0.5;
            var penetrationFixingDistance = minimalNotCollidingCentersDistance - centersDistance;

            if (penetrationFixingDistance < 0)
            {
                penetrationFixingDistance = 0;
            }

            return penetrationFixingDistance;
        }

        private static Vector2 ResolveCollisionWithBorders(RectangleColliderComponent collider, CharacterPhysicsComponent characterPhysics,
            Vector2 translationDelta)
        {
            if (CharacterIsCollidingWithLeftBorder(collider) && translationDelta.X < 0)
            {
                characterPhysics.Velocity = characterPhysics.Velocity.WithX(0);
                return translationDelta.WithX(0);
            }

            if (CharacterIsCollidingWithRightBorder(collider) && translationDelta.X > 0)
            {
                characterPhysics.Velocity = characterPhysics.Velocity.WithX(0);
                return translationDelta.WithX(0);
            }

            return translationDelta;
        }

        private static Vector2 ResolveCollisionWithOtherCharacter(Transform2DComponent transform, RectangleColliderComponent collider,
            CharacterPhysicsComponent characterPhysics, Vector2 translationDelta)
        {
            if (CharacterIsCollidingWithOtherCharacter(collider, out var otherCharacterEntity))
            {
                var otherTransform = otherCharacterEntity.GetComponent<Transform2DComponent>();
                var penetrationFixingDistance = ComputeRightCharacterToLeftCharacterPenetrationFixingDistance(transform, collider, otherCharacterEntity);

                var otherCharacterIsOnTheLeft = transform.Translation.X > otherTransform.Translation.X;
                if (otherCharacterIsOnTheLeft)
                {
                    //characterPhysics.Velocity = characterPhysics.Velocity.WithX(0);
                    return translationDelta.WithX(penetrationFixingDistance);
                }

                var otherCharacterIsOnTheRight = transform.Translation.X < otherTransform.Translation.X;
                if (otherCharacterIsOnTheRight)
                {
                    //characterPhysics.Velocity = characterPhysics.Velocity.WithX(0);
                    return translationDelta.WithX(-penetrationFixingDistance);
                }
            }

            return translationDelta;
        }

        private static double ComputeRightCharacterToLeftCharacterPenetrationFixingDistance(Transform2DComponent transform, RectangleColliderComponent collider,
            Entity otherCharacterEntity)
        {
            const double penetrationTolerance = 1;

            var otherTransform = otherCharacterEntity.GetComponent<Transform2DComponent>();
            var otherCollider = otherCharacterEntity.GetComponent<RectangleColliderComponent>();

            var centersDistance = Math.Abs(transform.Translation.X - otherTransform.Translation.X);
            var minimalNotCollidingCentersDistance = collider.Dimensions.X * 0.5 + otherCollider.Dimensions.X * 0.5;
            var penetrationFixingDistance = minimalNotCollidingCentersDistance - centersDistance;

            if (penetrationFixingDistance < penetrationTolerance)
            {
                penetrationFixingDistance = penetrationTolerance;
            }

            return penetrationFixingDistance;
        }

        private static void UpdatePosition(Transform2DComponent transform, Vector2 translationDelta)
        {
            transform.Translation += translationDelta;
        }

        private static bool CharacterIsCollidingWithGround(RectangleColliderComponent collider, [MaybeNullWhen(false)] out Entity groundEntity)
        {
            groundEntity = default;

            if (collider.IsColliding && collider.CollidingEntities.Any(e => e.Name == EntityName.Ground))
            {
                groundEntity = collider.CollidingEntities.Single(e => e.Name == EntityName.Ground);
                return true;
            }
            else
            {
                return false;
            }
        }

        private static bool CharacterIsCollidingWithLeftBorder(RectangleColliderComponent collider)
        {
            return collider.IsColliding && collider.CollidingEntities.Any(e => e.Name == EntityName.LeftBorder);
        }

        private static bool CharacterIsCollidingWithRightBorder(RectangleColliderComponent collider)
        {
            return collider.IsColliding && collider.CollidingEntities.Any(e => e.Name == EntityName.RightBorder);
        }

        private static bool CharacterIsCollidingWithOtherCharacter(RectangleColliderComponent collider, [MaybeNullWhen(false)] out Entity otherCharacterEntity)
        {
            otherCharacterEntity = default;

            if (collider.IsColliding && collider.CollidingEntities.Any(e => e.HasComponent<CharacterPhysicsComponent>()))
            {
                otherCharacterEntity = collider.CollidingEntities.Single(e => e.HasComponent<CharacterPhysicsComponent>());
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}