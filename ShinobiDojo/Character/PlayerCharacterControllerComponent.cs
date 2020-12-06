using System.Diagnostics;
using Geisha.Common.Math;
using Geisha.Engine.Core.Components;
using Geisha.Engine.Input.Components;

namespace ShinobiDojo.Character
{
    internal sealed class PlayerCharacterControllerComponent : BehaviorComponent
    {
        private InputComponent _inputComponent = null!;
        private CharacterPhysicsComponent _characterPhysicsComponent = null!;

        public override void OnStart()
        {
            Debug.Assert(Entity != null, nameof(Entity) + " != null");

            _inputComponent = Entity.GetComponent<InputComponent>();
            _characterPhysicsComponent = Entity.GetComponent<CharacterPhysicsComponent>();
        }

        public override void OnFixedUpdate()
        {
            _characterPhysicsComponent.Velocity = new Vector2(0, 0);

            if (_inputComponent.HardwareInput.KeyboardInput.Left)
            {
                _characterPhysicsComponent.Velocity = new Vector2(-100, 0);
            }

            if (_inputComponent.HardwareInput.KeyboardInput.Right)
            {
                _characterPhysicsComponent.Velocity = new Vector2(100, 0);
            }
        }
    }
}