using Microsoft.Xna.Framework;
using System;

namespace Embersite
{
    [Serializable]
    public class TimerClass
    {
        float Time = 0;
        float TimerMax = 0;
        bool Trigger = false;

        public bool IsTriggered
        {
            get
            {
                return Trigger;
            }
            set
            {

            }
        }

        public float PercentageDone
        {
            get
            {
                float Percent = Time / TimerMax;
                if (Percent > 1)
                {
                    return 1;
                }
                else
                {
                    return Percent;
                }
            }
        }

        public void ResetTrigger()
        {
            Trigger = false;
            Time = 0;
        }
        //Measured in Seconds
        public TimerClass(float MaxTime)
        {
            if (MaxTime > 0)
            {
                TimerMax = MaxTime;
            }
            else
            {
                throw new Exception("Timer Max Value Cannot Be Less than 0");
            }
        }

        public void Update(float gameTime)
        {
            if ((!Trigger) && (Time > TimerMax))
            {
                Trigger = true;
                Time = 0;
            }
            else
            {
                if (!Trigger)
                    Time += (float)gameTime;
            }



        }
    }
}
