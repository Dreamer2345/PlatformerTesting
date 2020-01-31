using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using PlatformerTesting.ObjectUtils.BaseObjectTypes;
using PlatformerTesting.Utils;
using System;

namespace PlatformerTesting.Objects
{
    class Player : DynamicObject
    {
        const float MaxHorizontalSpeed = 3;
        public Player(Vector2 Position, Rectangle CollisionBox)
        {
            GetCollider = CollisionBox;
            GetPosition = Position;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Globals.box, Collider, Color.Blue);
        }

        bool HitLeftWall = false;
        bool HitRightWall = false;
        bool Grounded = false;

        public override void OnCollide(DynamicObject obj)
        {
            if (obj is Terrain)
            {

                if (Acceleration.X != 0)
                {
                    GetPosition += new Vector2(-Acceleration.X, 0);
                    
                    if (Acceleration.X > 0)
                    {
                        HitRightWall = true;
                    }
                    else
                    {
                        HitLeftWall = true;
                    }

                    Acceleration.X = 0;
                }

                float halfHeight = obj.GetCollider.Height / 2;
                float halfHeightMe = GetCollider.Height / 2;

                float DistY = (float)Math.Abs(obj.GetCollider.Center.Y - GetCollider.Center.Y);
                float DistValY = halfHeight + halfHeightMe;


                if (DistY < DistValY)
                {
                    float diff = (float)Math.Abs(DistValY - DistY);
                    GetPosition = new Vector2(GetPosition.X, GetPosition.Y - (diff));
                    Acceleration *= new Vector2(1, 0);
                }

                
                Grounded = true;
            }
        }



        public override void Update(float Delta)
        {
            if (!Grounded)
            {
                HitLeftWall = false;
                HitRightWall = false;
                Acceleration += new Vector2(0, 6f) * Delta;
            }

            if (Globals.keyBoard.IsClicked(Keys.Space) && Grounded)
            {
                Acceleration += new Vector2(0, -5);
                Grounded = false;
            }

            bool Moving = false;
            if (Globals.keyBoard.Current.IsKeyDown(Keys.D))
            {
                Acceleration += new Vector2(3, 0);
                Moving = true;
            }

            if (Globals.keyBoard.Current.IsKeyDown(Keys.A))
            {
                Acceleration += new Vector2(-3, 0);
                Moving = true;
            }

            if (!Moving)
            {
                Acceleration *= new Vector2(0.98f, 1);
            }

            


            if (Math.Abs(Acceleration.X) > MaxHorizontalSpeed)
            {
                Vector2 normalised = Acceleration;
                normalised.Normalize();
                Acceleration.X = normalised.X * MaxHorizontalSpeed;
            }

            base.Update(Delta);

            Globals.camera.Position = position;
        }

        public override void AfterUpdate(float Delta)
        {
            if (!objectHandler.CheckPoint(this, new Vector2(Collider.Y, Collider.Bottom)) && !objectHandler.CheckPoint(this, new Vector2(Collider.Right, Collider.Bottom)) && (Grounded == true))
            {
                Grounded = false;
            }
        }
    }
}
