using Microsoft.Xna.Framework;

namespace PlatformerTesting.ObjectUtils.BaseObjectTypes
{
    class DynamicObject : BaseObject
    {

        public bool IsCollidable = true;

        protected Vector2 Accelleration;
        protected float Mass = 1;
        protected Rectangle Collider;

        public override Vector2 GetPosition { get => position; set { position = value; Collider.Location = value.ToPoint(); } }
        public virtual Vector2 GetAcceleration { get => Accelleration; set => Accelleration = value; }
        public virtual float GetMass { get => Mass; set => Mass = value; }
        public virtual Rectangle GetCollider { get => Collider; set { Collider = value; Collider.Location = position.ToPoint(); } }

        public override void Update(float Delta)
        {
            GetPosition += Accelleration * Delta * 10;
        }
        public virtual void OnCollide(DynamicObject collided) { }
    }
}
