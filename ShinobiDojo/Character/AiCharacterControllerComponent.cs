using System.Diagnostics;
using Geisha.Engine.Core;
using Geisha.Engine.Core.Components;

namespace ShinobiDojo.Character
{
    internal sealed class AiCharacterControllerComponent : BehaviorComponent
    {
        private CharacterControllerComponent _characterControllerComponent = null!;

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
}