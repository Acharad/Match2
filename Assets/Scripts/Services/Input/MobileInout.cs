using UnityEngine;

namespace Services.Input
{
    public class MobileInout : IInput
    {
        public Touch GetTouchState()
        {
            if (UnityEngine.Input.touchCount <= 1) return Touch.None;

            var touch = UnityEngine.Input.GetTouch(0);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    return Touch.Started;
                case TouchPhase.Moved:
                    return Touch.Moved;
                case TouchPhase.Ended:
                    return Touch.Ended;
            }

            return Touch.None;
        }

        public Vector2 GetTouchPosition()
        {
            return UnityEngine.Input.GetTouch(0).position;
        }
    }
}