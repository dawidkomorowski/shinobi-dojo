using Geisha.Engine.Core.Components;
using Geisha.Engine.Core.Math;
using Geisha.Engine.Core.SceneModel;
using Geisha.Engine.Input.Components;
using Geisha.Engine.Physics.Components;
using Geisha.Engine.Rendering.Components;

namespace ShinobiDojo.Character
{
    internal static class CharacterEntityFactory
    {
        public static void CreatePlayerCharacter(Scene scene, double xPos)
        {
            var characterEntity = CreateCharacter(scene, xPos, Color.FromArgb(255, 255, 255, 0));

            characterEntity.CreateComponent<InputComponent>();
            characterEntity.CreateComponent<PlayerCharacterControllerComponent>();
        }

        public static void CreateAiCharacter(Scene scene, double xPos)
        {
            var characterEntity = CreateCharacter(scene, xPos, Color.FromArgb(255, 127, 127, 127));

            characterEntity.CreateComponent<AiCharacterControllerComponent>();
        }

        private static Entity CreateCharacter(Scene scene, double xPos, Color color)
        {
            var characterEntity = scene.CreateEntity();

            var transform = characterEntity.CreateComponent<Transform2DComponent>();
            transform.Translation = new Vector2(xPos, 500);
            transform.Rotation = 0;
            transform.Scale = Vector2.One;

            var dimensions = new Vector2(128, 256);
            var rectangleRenderer = characterEntity.CreateComponent<RectangleRendererComponent>();
            rectangleRenderer.Color = color;
            rectangleRenderer.Dimension = dimensions;
            rectangleRenderer.FillInterior = true;

            var rectangleCollider = characterEntity.CreateComponent<RectangleColliderComponent>();
            rectangleCollider.Dimensions = dimensions;

            characterEntity.CreateComponent<CharacterPhysicsComponent>();
            characterEntity.CreateComponent<CharacterControllerComponent>();

            return characterEntity;
        }
    }
}