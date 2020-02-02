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
        const float MaxHorizontalSpeed = 10;
        public Player(Vector2 Position, Rectangle CollisionBox)
        {
            GetCollider = CollisionBox;
            GetPosition = Position;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Globals.box, Collider, Color.Blue);
        }


        bool CollidedUp = false;
        bool CollidedDown = false;
        bool CollidedLeft = false;
        bool CollidedRight = false;


        public override void OnCollide(DynamicObject obj)
        {
            CollidedLeft = false;
            CollidedRight = false;
            CollidedUp = false;
            CollidedDown = false;
            if (obj is Terrain)
            {

                Rectangle Overlap = Rectangle.Intersect(obj.GetCollider, GetCollider);

                float PercentX = 0;
                float PercentY = 0;

                PercentX = (float)Overlap.Width / (float)obj.GetCollider.Width;
                PercentY = (float)Overlap.Height / (float)obj.GetCollider.Height;


                if (PercentX > PercentY)
                {
                    if (obj.GetCollider.Center.X < GetCollider.X)
                    {
                        Acceleration.X = Math.Max(Acceleration.X, 0);
                        CollidedRight = true;

                    }
                    else
                    {
                        Acceleration.X = Math.Min(Acceleration.X, 0);
                        CollidedLeft = true;
                    }
                }
                else
                {
                    if (obj.GetCollider.Center.Y < GetCollider.Y)
                    {
                        Acceleration.Y = Math.Max(Acceleration.Y, 0);
                        CollidedUp = true;
                    }
                    else
                    {
                        Acceleration.Y = Math.Min(Acceleration.Y, 0);
                        CollidedDown = true;
                    }
                }

                if(PercentX < PercentY)
                    GetPosition -= new Vector2(0, Overlap.Height);
                else
                    if(CollidedRight)
                        GetPosition -= new Vector2(Overlap.Width, 0);
                    else
                        GetPosition += new Vector2(Overlap.Width, 0);
                Console.WriteLine(PercentX + ":" + PercentY);

            }
        }



        public override void Update(float Delta)
        {
            if (!CollidedDown)
            {
                Acceleration += new Vector2(0, 0.5f);
            }

            if (Globals.keyBoard.IsClicked(Keys.Space) && CollidedDown)
            {
                Acceleration += new Vector2(0, -20);
                CollidedDown = false;
            }

            bool Moving = false;
            if (Globals.keyBoard.Current.IsKeyDown(Keys.D) && !CollidedRight)
            {
                Acceleration += new Vector2(5, 0);
                Moving = true;
            }

            if (Globals.keyBoard.Current.IsKeyDown(Keys.A) && !CollidedLeft)
            {
                Acceleration += new Vector2(-5, 0);
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

            if (!objectHandler.CheckPoint(this, new Vector2(GetCollider.Center.X, GetCollider.Bottom + 2)) && !objectHandler.CheckPoint(this, new Vector2(GetCollider.Right, GetCollider.Bottom + 2)) && !objectHandler.CheckPoint(this, new Vector2(GetCollider.Left, GetCollider.Bottom + 2)))
            {
                CollidedDown = false;
            }


            Globals.camera.Position = position;
        }
    }
}
