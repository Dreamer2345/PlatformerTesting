using Microsoft.Xna.Framework;
using PlatformerTesting.ObjectUtils.BaseObjectTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlatformerTesting.Objects
{
    class RigidPhysicsObject : DynamicObject
    {
        protected Vector2 PreviousPos;
        protected Vector2 PreviousAcceleration;
        protected Rectangle PreviousCollider;

        bool HasGrav = true;

        protected bool CollidedLastFrame = false;

        protected bool CollidedUp = false;
        protected bool CollidedDown = false;
        protected bool CollidedLeft = false;
        protected bool CollidedRight = false;

        protected bool PCollidedUp = false;
        protected bool PCollidedDown = false;
        protected bool PCollidedLeft = false;
        protected bool PCollidedRight = false;

        public void PreUpdate(float Delta)
        {
            PreviousAcceleration = Accelleration;
            PreviousPos = GetPosition;
            PreviousCollider = GetCollider;

            PCollidedDown = CollidedDown;
            PCollidedLeft = CollidedLeft;
            PCollidedRight = CollidedRight;
            PCollidedUp = CollidedUp;

            CollidedLeft = false;
            CollidedRight = false;

        }

        public override void Update(float Delta)
        {
            GetPosition += (Accelleration / 2 + PreviousAcceleration / 2) * Delta * 10;

            if (HasGrav)
            {
                if(!objectHandler.CheckPoint(this, new Vector2(GetPosition.X, GetCollider.Bottom + 1)) && !objectHandler.CheckPoint(this, new Vector2(GetPosition.X + GetCollider.Width, GetCollider.Bottom + 1)))
                {
                    CollidedDown = false;
                }
            }
        }

        bool CollideRight(DynamicObject obj)
        {
            if (GetCollider.Right > obj.GetCollider.Left && PreviousCollider.Right <= obj.GetCollider.Left)
            {
                Accelleration.X = 0;
                position.X = obj.GetPosition.X - GetCollider.Width - 0.01f;
                CollidedLastFrame = true;
                return true;
            }
            return false;
        }

        bool CollideLeft(DynamicObject obj)
        {
            if (GetCollider.Left < obj.GetCollider.Right && PreviousCollider.Left >= obj.GetCollider.Right)
            {
                Accelleration.X = 0;
                position.X = obj.GetPosition.X + obj.GetCollider.Width + 0.01f;
                return true;
            }
            return false;
        }

        bool CollideTop(DynamicObject obj)
        {
            if(GetCollider.Top < obj.GetCollider.Bottom && PreviousCollider.Top >= obj.GetCollider.Bottom)
            {
                Accelleration.Y = 0;
                position.Y = obj.GetPosition.Y + obj.GetCollider.Height;
                return true;
            }
            return false;
        }

        bool CollideBottom(DynamicObject obj)
        {
            if (GetCollider.Bottom > obj.GetCollider.Top && PreviousCollider.Bottom >= obj.GetCollider.Top)
            {
                Accelleration.Y = 0;
                position.Y = obj.GetPosition.Y - GetCollider.Height - 0.5f;
                return true;
            }
            return false;
        }

        public override void OnCollide(DynamicObject obj)
        {
            if (!obj.IsCollidable)
                return;
            if (!PCollidedLeft)
                CollidedLeft = CollideLeft(obj);
            else
                CollidedLeft = false;

            if (!PCollidedRight)
                CollidedRight = CollideRight(obj);
            else
                CollidedRight = false;

            if (!CollidedRight && !CollidedLeft)
            {
                CollidedUp = CollideTop(obj);
                if(!CollidedUp)
                    CollidedDown = CollideBottom(obj);
            }
        }
    }
}
