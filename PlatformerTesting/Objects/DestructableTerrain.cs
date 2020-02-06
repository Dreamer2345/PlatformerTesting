using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlatformerTesting.Objects
{
    class DestructableTerrain : Terrain
    {
        public DestructableTerrain(Vector2 Pos, Rectangle CollisionBox) : base(Pos, CollisionBox)
        {

        }
    }
}
