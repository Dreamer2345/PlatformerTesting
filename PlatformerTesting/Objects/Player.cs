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
        int Health = 100;

        Vector2 StartPos = Vector2.Zero;

        const float HorizontalAcceleration = 1f;
        const float JumpAcceleration = -30;
        const float MaxHorizontalSpeed = 15;

        int Facing = 0;

        public void Reset()
        {
            PreviousAcceleration = StartPos;
            PreviousPos = StartPos;
            GetPosition = StartPos;
            Accelleration = Vector2.Zero;
            Health = 100;
        }

        public Player(Vector2 Position, Rectangle CollisionBox)
        {
            GetCollider = CollisionBox;
            GetPosition = Position;
            StartPos = Position;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(Globals.font, "Health:" + Health, (position + PreviousPos) / 2 + new Vector2(0, -20), Color.Black);
            spriteBatch.Draw(Globals.box, Collider, Color.Blue);
            //spriteBatch.Draw(Globals.box, new Vector2(Collider.X + 16 * Facing, Collider.Center.Y - 16), Color.Red);
        }

        public override void Update(float Delta)
        {
            PreUpdate(Delta);

            if(position.Y > 1000)
            {
                Health -= 100;
            }

            if(Health <= 0)
            {
                Reset();
            }

            if (!CollidedDown && !PCollidedDown)
            {
                Accelleration += new Vector2(0, 0.5f);
            }

            if (Globals.keyBoard.IsClicked(Keys.Space) && CollidedDown)
            {
                Accelleration += new Vector2(0, JumpAcceleration);
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
                Accelleration += new Vector2(HorizontalAcceleration, 0);
                CollidedLeft = false;
                Moving = true;
            }

            if (Globals.keyBoard.Current.IsKeyDown(Keys.A) && !PCollidedLeft)
            {
                Facing = -1;
                Accelleration += new Vector2(-HorizontalAcceleration, 0);
                CollidedRight = false;
                Moving = true;
            }

            DynamicObject Bellow = objectHandler.GetPoint(this, new Vector2(GetCollider.Center.X, GetCollider.Bottom + 1));
            if (Bellow is MovingPlatform)
            {
                GetPosition += Bellow.GetAcceleration;
            }

            if (!Moving)
            {
                Accelleration *= new Vector2(0.85f, 1);
            }

            if (Math.Abs(Accelleration.X) > MaxHorizontalSpeed)
            {
                Vector2 normalised = Accelleration;
                normalised.Normalize();
                Accelleration.X = normalised.X * MaxHorizontalSpeed;
            }


            


            base.Update(Delta);
            Globals.camera.Position = (PreviousPos + position)/2;
        }
    }
}
