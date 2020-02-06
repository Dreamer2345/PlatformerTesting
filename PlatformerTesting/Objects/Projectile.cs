using Embersite;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PlatformerTesting.ObjectUtils.BaseObjectTypes;
using PlatformerTesting.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlatformerTesting.Objects
{
    class Projectile : DynamicObject
    {
        TimerClass lifeTimer = new TimerClass(5f);
        public Projectile(Vector2 position, Vector2 Direction)
        {
            IsCollidable = false;
            GetCollider = new Rectangle(0, 0, 16, 16);
            GetPosition = position;
            GetAcceleration = Direction;
        }

        public override void Update(float Delta)
        {
            lifeTimer.Update(Delta);

            if (lifeTimer.IsTriggered)
            {
                GetDestroyed = true;
            }
            base.Update(Delta);
        }

        public override void OnCollide(DynamicObject collided)
        {
            if (collided is Terrain)
            {
                GetDestroyed = true;
            }

            if (collided is DestructableTerrain)
            {
                collided.GetDestroyed = true;
                GetDestroyed = true;
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Globals.box, Collider, Color.Blue);
        }
    }
}
