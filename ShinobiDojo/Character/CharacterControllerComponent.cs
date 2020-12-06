using Geisha.Common.Math;
using Geisha.Engine.Core.SceneModel;

namespace ShinobiDojo.Character
{
    internal sealed class CharacterControllerComponent : IComponent
    {
        private const int WalkSpeed = 100;

        private bool _isWalkLeft;
        private bool _isWalkRight;

        public void WalkLeft()
        {
            _isWalkLeft = true;
        }

        public void WalkRight()
        {
            _isWalkRight = true;
        }

        internal void Process(CharacterPhysicsComponent characterPhysicsComponent)
        {
            characterPhysicsComponent.Velocity = new Vector2(0, 0);

            if (_isWalkLeft)
            {
                characterPhysicsComponent.Velocity = new Vector2(-WalkSpeed, 0);
            }

            if (_isWalkRight)
            {
                characterPhysicsComponent.Velocity = new Vector2(WalkSpeed, 0);
            }

            _isWalkLeft = false;
            _isWalkRight = false;
        }
    }
}