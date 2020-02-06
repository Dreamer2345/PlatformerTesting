using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using PlatformerTesting.ObjectUtils.BaseObjectTypes;
using PlatformerTesting.Utils;
using System;

namespace PlatformerTesting.Objects
{
    class Player : RigidPhysicsObject
    {
        const float HorizontalAcceleration = 1f;
        const float JumpAcceleration = -30;
        const float MaxHorizontalSpeed = 15;

        int Facing = 0;

        public Player(Vector2 Position, Rectangle CollisionBox)
        {
            GetCollider = CollisionBox;
            GetPosition = Position;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Globals.box, Collider, Color.Blue);
            spriteBatch.Draw(Globals.box, new Vector2(Collider.X + 16 * Facing, Collider.Center.Y - 16), Color.Red);
        }

        public override void Update(float Delta)
        {

            //Console.WriteLine("Frame:" + CollidedLastFrame);
            //Console.WriteLine("Up:" + CollidedUp);
            //Console.WriteLine("Down:" + CollidedDown);
            //Console.WriteLine("Left:" + CollidedLeft);
            //Console.WriteLine("Right:" + CollidedRight);

            PreUpdate(Delta);

            if (!CollidedDown && !PCollidedDown)
            {
                Acceleration += new Vector2(0, 0.5f);
            }

            if (Globals.keyBoard.IsClicked(Keys.Space) && CollidedDown)
            {
                Acceleration += new Vector2(0, JumpAcceleration);
                CollidedDown = false;
            }

            if (Globals.keyBoard.IsClicked(Keys.E))
            {
                objectHandler.Add(new Projectile(GetPosition, new Vector2(MaxHorizontalSpeed + 5, 0) * Facing));
            }

            bool Moving = false;
            if (Globals.keyBoard.Current.IsKeyDown(Keys.D) && !PCollidedRight)
            {
                Facing = 1;
                Acceleration += new Vector2(HorizontalAcceleration, 0);
                CollidedLeft = false;
                Moving = true;
            }

            if (Globals.keyBoard.Current.IsKeyDown(Keys.A) && !PCollidedLeft)
            {
                Facing = -1;
                Acceleration += new Vector2(-HorizontalAcceleration, 0);
                CollidedRight = false;
                Moving = true;
            }

            if (!Moving)
            {
                Acceleration *= new Vector2(0.85f, 1);
            }

            if (Math.Abs(Acceleration.X) > MaxHorizontalSpeed)
            {
                Vector2 normalised = Acceleration;
                normalised.Normalize();
                Acceleration.X = normalised.X * MaxHorizontalSpeed;
            }
            //Console.WriteLine(Acceleration);

            

            base.Update(Delta);
            Globals.camera.Position = (PreviousPos + position)/2;
        }
    }
}
