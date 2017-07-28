using Microsoft.Xna.Framework.Input;

namespace Strike2D
{
    public class InputManager
    {
        private KeyboardState keyState, prevKeyState;
        private MouseState mouseState, prevMouseState;

        public InputManager()
        {
            keyState = prevKeyState = Keyboard.GetState();
            mouseState = prevMouseState = Mouse.GetState();
            Debug.WriteLine("Input Manager Initialized.");
        }

        public void Tick()
        {
            keyState = Keyboard.GetState();
            mouseState = Mouse.GetState();
        }

        public void Tock()
        {
            prevKeyState = keyState;
            prevMouseState = mouseState;
        }
    }
}