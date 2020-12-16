﻿using Geisha.Common.Math;
using Geisha.Engine.Core.SceneModel;

namespace ShinobiDojo.Character
{
    internal sealed class CharacterControllerComponent : IComponent
    {
        private const double WalkSpeed = 400;
        private const double JumpSpeed = 1900;

        private bool _isWalkLeft;
        private bool _isWalkRight;
        private bool _isJump;
        private bool _readyForJump;

        public void WalkLeft()
        {
            _isWalkLeft = true;
        }

        public void WalkRight()
        {
            _isWalkRight = true;
        }

        public void Jump()
        {
            _isJump = true;
        }

        internal void Process(CharacterPhysicsComponent characterPhysicsComponent)
        {
            if (characterPhysicsComponent.StandingOnTheGround)
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

                if (_readyForJump && _isJump)
                {
                    _readyForJump = false;
                    characterPhysicsComponent.Velocity += new Vector2(0, JumpSpeed);
                }
            }
            else
            {
                _readyForJump = true;
            }

            _isWalkLeft = false;
            _isWalkRight = false;
            _isJump = false;
        }
    }
}