using Geisha.Common.Math;
using Geisha.Engine.Core.SceneModel;

namespace ShinobiDojo.Character
{
    internal sealed class CharacterPhysicsComponent : IComponent
    {
        public Vector2 Velocity { get; set; }
    }
}