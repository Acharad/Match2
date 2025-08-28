using System;
using UnityEngine;

namespace Services.Input
{
    public class InputListener: MonoBehaviour
    {
        public Action<Vector3> TouchStarted;
        public Action<Vector3> TouchMoved;
        public Action<Vector3> TouchEnded;
        
        private IInput _input;
        private bool _listenerEnable;

        private void Awake()
        {
#if UNITY_EDITOR
            _input = new MouseInput();
            Debug.Log("InputListener | Mouse input");
#else
            _input = new TouchInput();
            Debug.Log("InputListener | Touch input");
#endif
        }


        public void Init()
        {
            _listenerEnable = true;
        }

        private void Update()
        {
            if (!_listenerEnable) return;

            if (_input.GetTouchState() == Touch.None) return;

            switch (_input.GetTouchState())
            {
                case Touch.Started:
                    TouchStarted?.Invoke(_input.GetTouchPosition());
                    break;
                case Touch.Moved:
                    TouchMoved?.Invoke(_input.GetTouchPosition());
                    break;
                case Touch.Ended:
                    TouchEnded?.Invoke(_input.GetTouchPosition());
                    break;
                
            }
            
        }
    }
}