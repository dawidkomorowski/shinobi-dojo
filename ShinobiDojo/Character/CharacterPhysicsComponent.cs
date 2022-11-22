using Geisha.Engine.Core.Math;
using Geisha.Engine.Core.SceneModel;

namespace ShinobiDojo.Character
{
    internal sealed class CharacterPhysicsComponent : Component
    {
        public CharacterPhysicsComponent(Entity entity) : base(entity)
        {
        }

        public bool StandingOnTheGround { get; set; }

        public Vector2 Velocity { get; set; }
    }

    internal sealed class CharacterPhysicsComponentFactory : ComponentFactory<CharacterPhysicsComponent>
    {
        protected override CharacterPhysicsComponent CreateComponent(Entity entity) => new(entity);
    }
}