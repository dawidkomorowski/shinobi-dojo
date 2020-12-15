using Geisha.Common.Math;
using Geisha.Engine.Core.Components;
using Geisha.Engine.Core.SceneModel;
using Geisha.Engine.Input.Components;
using Geisha.Engine.Physics.Components;
using Geisha.Engine.Rendering.Components;

namespace ShinobiDojo.Character
{
    internal sealed class CharacterEntityFactory
    {
        public Entity CreatePlayerCharacter(double xPos)
        {
            var characterEntity = CreateCharacter(xPos, Color.FromArgb(255, 255, 255, 0));

            characterEntity.AddComponent(new InputComponent());
            characterEntity.AddComponent(new PlayerCharacterControllerComponent());

            return characterEntity;
        }

        public Entity CreateAiCharacter(double xPos)
        {
            var characterEntity = CreateCharacter(xPos, Color.FromArgb(255, 127, 127, 127));

            return characterEntity;
        }

        private static Entity CreateCharacter(double xPos, Color color)
        {
            var characterEntity = new Entity();

            characterEntity.AddComponent(new Transform2DComponent
            {
                Translation = new Vector2(xPos, 500),
                Rotation = 0,
                Scale = Vector2.One
            });

            var dimension = new Vector2(128, 256);
            characterEntity.AddComponent(new RectangleRendererComponent
            {
                Color = color,
                Dimension = dimension,
                FillInterior = true
            });
            characterEntity.AddComponent(new RectangleColliderComponent
            {
                Dimension = dimension
            });

            characterEntity.AddComponent(new CharacterPhysicsComponent());
            characterEntity.AddComponent(new CharacterControllerComponent());

            return characterEntity;
        }
    }
}