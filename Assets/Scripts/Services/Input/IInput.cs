using UnityEngine;

namespace Services.Input
{
    public interface IInput
    {
        public Touch GetTouchState();
        public Vector2 GetTouchPosition();
    }
}
