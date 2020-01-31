using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PlatformerTesting.ObjectUtils.BaseObjectTypes;
using PlatformerTesting.Utils;

namespace PlatformerTesting.Objects
{
    class Terrain : DynamicObject
    {


        public Terrain(Vector2 Position, Rectangle CollisionBox)
        {
            GetCollider = CollisionBox;
            GetPosition = Position;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Globals.box, GetCollider, Color.White);
        }

        public override void Update(float Delta)
        {
            
        }
    }
}
