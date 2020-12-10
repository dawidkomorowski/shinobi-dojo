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
        public Entity CreatePlayerCharacter()
        {
            var characterEntity = CreateCharacter();

            characterEntity.AddComponent(new InputComponent());
            characterEntity.AddComponent(new PlayerCharacterControllerComponent());

            return characterEntity;
        }

        private Entity CreateCharacter()
        {
            var characterEntity = new Entity();

            characterEntity.AddComponent(new Transform2DComponent
            {
                Translation = new Vector2(0, 500),
                Rotation = 0,
                Scale = Vector2.One
            });

            var dimension = new Vector2(128, 256);
            characterEntity.AddComponent(new RectangleRendererComponent
            {
                Color = Color.FromArgb(255, 255, 255, 0),
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