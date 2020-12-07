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
            var initialVelocity = characterPhysicsComponent.Velocity;
            characterPhysicsComponent.Velocity = initialVelocity.WithX(0);

            if (_isWalkLeft)
            {
                characterPhysicsComponent.Velocity = initialVelocity.WithX(-WalkSpeed);
            }

            if (_isWalkRight)
            {
                characterPhysicsComponent.Velocity = initialVelocity.WithX(WalkSpeed);
            }

            _isWalkLeft = false;
            _isWalkRight = false;
        }
    }
}