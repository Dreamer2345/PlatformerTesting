using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PlatformerTesting.Objects;

namespace PlatformerTesting.ObjectUtils.BaseObjectTypes
{
    class BaseObject
    {
        protected ObjectHandler objectHandler;

        public void SetObjectHandler(ObjectHandler obj)
        {
            if (objectHandler == null)
                objectHandler = obj;
        }

        protected bool Destroyed = false;
        public bool GetDestroyed { get => Destroyed; set => Destroyed = value; }

        protected Vector2 position;
        public virtual Vector2 GetPosition { get { return position; } set { position = value; } }

        public virtual void OnCreate() { }
        public virtual void OnDestroy() { }
        public virtual void Update(float Delta) { }
        public virtual void AfterUpdate(float Delta) { }
        public virtual void Draw(SpriteBatch spriteBatch) { }

    }
}
