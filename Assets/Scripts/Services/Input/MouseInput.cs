using UnityEngine;

namespace Services.Input
{
    public class MouseInput : IInput
    {
        public Touch GetTouchState()
        {
            if (UnityEngine.Input.GetMouseButtonDown(0))
                return Touch.Started;
            if (UnityEngine.Input.GetMouseButton(0))
                return Touch.Moved;
            if (UnityEngine.Input.GetMouseButtonUp(0))
                return Touch.Ended;

            return Touch.None;
        }

        public Vector2 GetTouchPosition()
        {
            return UnityEngine.Input.mousePosition;
        }
    }
}