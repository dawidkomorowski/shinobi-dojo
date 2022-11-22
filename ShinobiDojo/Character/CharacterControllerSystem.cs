using System.Linq;
using Geisha.Engine.Core;
using Geisha.Engine.Core.SceneModel;
using Geisha.Engine.Core.Systems;

namespace ShinobiDojo.Character
{
    internal sealed class CharacterControllerSystem : ICustomSystem
    {
        #region ICustomSystem implementation

        private Scene? _scene;

        public string Name => "ShinobiDojo.CharacterControllerSystem";

        public void ProcessFixedUpdate()
        {
            // TODO This is naive implementation to quickly migrate code from Geisha Engine 0.5 to Geisha Engine 0.8
            if (_scene is not null)
            {
                InternalProcessFixedUpdate(_scene);
            }
        }

        public void ProcessUpdate(GameTime gameTime)
        {
        }

        public void OnEntityCreated(Entity entity)
        {
            _scene = entity.Scene;
        }

        public void OnEntityRemoved(Entity entity)
        {
        }

        public void OnEntityParentChanged(Entity entity, Entity? oldParent, Entity? newParent)
        {
        }

        public void OnComponentCreated(Component component)
        {
        }

        public void OnComponentRemoved(Component component)
        {
        }

        #endregion

        public void InternalProcessFixedUpdate(Scene scene)
        {
            var entities = scene.RootEntities.Where(e =>
                e.HasComponent<CharacterPhysicsComponent>() && e.HasComponent<CharacterControllerComponent>()).ToList();

            foreach (var entity in entities)
            {
                var characterPhysics = entity.GetComponent<CharacterPhysicsComponent>();
                var characterController = entity.GetComponent<CharacterControllerComponent>();

                characterController.Process(characterPhysics);
            }
        }
    }
}