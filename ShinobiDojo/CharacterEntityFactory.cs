using Geisha.Common.Math;
using Geisha.Engine.Core.Components;
using Geisha.Engine.Core.SceneModel;
using Geisha.Engine.Rendering.Components;

namespace ShinobiDojo
{
    internal sealed class CharacterEntityFactory
    {
        public Entity CreateCharacter()
        {
            var characterEntity = new Entity();

            characterEntity.AddComponent(Transform2DComponent.CreateDefault());
            characterEntity.AddComponent(new RectangleRendererComponent
            {
                Color = Color.FromArgb(255, 255, 255, 0),
                Dimension = new Vector2(128, 256),
                FillInterior = true
            });

            return characterEntity;
        }
    }
}