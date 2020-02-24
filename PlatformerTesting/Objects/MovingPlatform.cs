using Embersite;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlatformerTesting.Objects
{
    class MovingPlatform : Terrain
    {
        Vector2 start, end , Acel;
        bool Direction = true;
        TimerClass timer;

        public MovingPlatform(Vector2 StartPosition , Vector2 EndPosition, float Time, Rectangle CollisionBox) : base(StartPosition, CollisionBox)
        {
            timer = new TimerClass(Time);
            start = StartPosition;
            end = EndPosition;
        }

        public override void Update(float Delta)
        {
            timer.Update(Delta);
            if (timer.IsTriggered)
            {
                Direction = !Direction;
                timer.ResetTrigger();
            }

            if (Direction)
            {
                Vector2 newPos = Vector2.Lerp(start, end, timer.PercentageDone);
                Accelleration = -(GetPosition - newPos);
                GetPosition = newPos;

            }
            else
            {
                Vector2 newPos = Vector2.Lerp(end, start, timer.PercentageDone);
                Accelleration = -(GetPosition - newPos);
                GetPosition = newPos;
            }



        }
    }
}
