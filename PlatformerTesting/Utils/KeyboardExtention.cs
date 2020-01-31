using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace PlatformerTesting.Utils
{
    public class KeyboardExtention
    {
        public KeyboardState LastKey { get; private set; }
        public KeyboardState Current { get; private set; }
        public bool KeyClicked;

        public bool IsClicked(Keys Key)
        {
            return (LastKey.IsKeyDown(Key) ^ Current.IsKeyDown(Key)) && Current.IsKeyDown(Key);
        }


        public void Update(GameTime gameTime)
        {
            LastKey = Current;
            Current = Keyboard.GetState();

            if ((LastKey.GetPressedKeys().Length == 0) && (Current.GetPressedKeys().Length > 0))
            {
                KeyClicked = true;
            }
            else
            {
                KeyClicked = false;
            }


        }
    }
}
