﻿using System.Diagnostics.CodeAnalysis;
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
                translationDelta = ResolveCollisionWithOtherCharacter(transform, collider, characterPhysics, translationDelta);
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
                if (CharacterIsCollidingWithGround(collider) == false)
                {
                    characterPhysics.StandingOnTheGround = false;
                }

                return translationDelta;
            }

            if (CharacterIsCollidingWithGround(collider))
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

                var otherCharacterIsOnTheLeft = transform.Translation.X > otherTransform.Translation.X;
                if (otherCharacterIsOnTheLeft && translationDelta.X < 0)
                {
                    characterPhysics.Velocity = characterPhysics.Velocity.WithX(0);
                    return translationDelta.WithX(0);
                }

                var otherCharacterIsOnTheRight = transform.Translation.X < otherTransform.Translation.X;
                if (otherCharacterIsOnTheRight && translationDelta.X > 0)
                {
                    characterPhysics.Velocity = characterPhysics.Velocity.WithX(0);
                    return translationDelta.WithX(0);
                }
            }

            return translationDelta;
        }

        private static void UpdatePosition(Transform2DComponent transform, Vector2 translationDelta)
        {
            transform.Translation += translationDelta;
        }

        private static bool CharacterIsCollidingWithGround(RectangleColliderComponent collider)
        {
            return collider.IsColliding && collider.CollidingEntities.Any(e => e.Name == EntityName.Ground);
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