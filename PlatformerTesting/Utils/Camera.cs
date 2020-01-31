using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlatformerTesting.Utils
{
    class Camera
    {
        int ViewWidth;
        int ViewHeight;
        Vector2 position;
        float rotation;
        float scale;

        public Camera(int viewWidth, int viewHeight)
        {
            Position = Vector2.Zero;
            rotation = 0;
            scale = 1;
            ViewWidth = viewWidth;
            ViewHeight = viewHeight;
        }

        public Vector2 Position { get => position; set => position = value; }
        public float Rotation { get => rotation; set => rotation = value; }
        public float Scale { get => scale; set => scale = value; }

        public Matrix GetMatrix
        {
            get
            {
                return Matrix.CreateTranslation(ViewWidth / 2, ViewHeight / 2, 0) * Matrix.CreateTranslation(-(int)Position.X,-(int)Position.Y, 0) * Matrix.CreateScale(Scale,Scale,1) * Matrix.CreateRotationZ(Rotation);
            }
        }
    }
}
