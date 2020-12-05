using System.Diagnostics;
using Geisha.Common.Math;
using Geisha.Engine.Core.Components;
using Geisha.Engine.Input.Components;

namespace ShinobiDojo.Character
{
    internal sealed class PlayerCharacterControllerComponent : BehaviorComponent
    {
        private InputComponent _inputComponent = null!;
        private Transform2DComponent _transform2DComponent = null!;

        public override void OnStart()
        {
            Debug.Assert(Entity != null, nameof(Entity) + " != null");

            _inputComponent = Entity.GetComponent<InputComponent>();
            _transform2DComponent = Entity.GetComponent<Transform2DComponent>();
        }

        public override void OnFixedUpdate()
        {
            if (_inputComponent.HardwareInput.KeyboardInput.Left)
            {
                _transform2DComponent.Translation += new Vector2(-5, 0);
            }

            if (_inputComponent.HardwareInput.KeyboardInput.Right)
            {
                _transform2DComponent.Translation += new Vector2(5, 0);
            }
        }
    }
}