using Geisha.Common.Math;
using Geisha.Engine.Core.SceneModel;

namespace ShinobiDojo.Character
{
    internal sealed class CharacterPhysicsComponent : IComponent
    {
        public bool StandingOnTheGround { get; set; }
        public Vector2 Velocity { get; set; }
    }
}