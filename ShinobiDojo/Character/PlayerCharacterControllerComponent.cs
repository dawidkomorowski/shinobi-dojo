using System.Diagnostics;
using Geisha.Engine.Core.Components;
using Geisha.Engine.Input.Components;

namespace ShinobiDojo.Character
{
    internal sealed class PlayerCharacterControllerComponent : BehaviorComponent
    {
        private InputComponent _inputComponent = null!;
        private CharacterControllerComponent _characterControllerComponent = null!;

        public override void OnStart()
        {
            Debug.Assert(Entity != null, nameof(Entity) + " != null");

            _inputComponent = Entity.GetComponent<InputComponent>();
            _characterControllerComponent = Entity.GetComponent<CharacterControllerComponent>();
        }

        public override void OnFixedUpdate()
        {
            if (_inputComponent.HardwareInput.KeyboardInput.Left)
            {
                _characterControllerComponent.WalkLeft();
            }

            if (_inputComponent.HardwareInput.KeyboardInput.Right)
            {
                _characterControllerComponent.WalkRight();
            }

            if (_inputComponent.HardwareInput.KeyboardInput.Up)
            {
                _characterControllerComponent.Jump();
            }
        }
    }
}