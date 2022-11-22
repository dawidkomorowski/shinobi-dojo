using System.Diagnostics;
using Geisha.Engine.Core;
using Geisha.Engine.Core.Components;
using Geisha.Engine.Core.SceneModel;

namespace ShinobiDojo.Character
{
    internal sealed class AiCharacterControllerComponent : BehaviorComponent
    {
        private CharacterControllerComponent _characterControllerComponent = null!;

        public AiCharacterControllerComponent(Entity entity) : base(entity)
        {
        }

        public override void OnStart()
        {
            Debug.Assert(Entity != null, nameof(Entity) + " != null");

            _characterControllerComponent = Entity.GetComponent<CharacterControllerComponent>();
        }

        public override void OnFixedUpdate()
        {
            if (GameTime.TimeSinceStartUp.Seconds % 2 == 0)
            {
                _characterControllerComponent.WalkLeft();
            }
            else
            {
                _characterControllerComponent.WalkRight();
            }
        }
    }

    internal sealed class AiCharacterControllerComponentFactory : ComponentFactory<AiCharacterControllerComponent>
    {
        protected override AiCharacterControllerComponent CreateComponent(Entity entity) => new(entity);
    }
}