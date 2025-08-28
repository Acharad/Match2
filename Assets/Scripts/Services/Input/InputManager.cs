using System;
using Game.Services;
using UnityEngine;

namespace Services.Input
{
    public class InputManager : IDisposable
    {
        private Camera _camera;

        private InputListener _inputListener;

        private MatchHandler _matchHandler;


        public void Init(InputListener inputListener, MatchHandler matchHandler)
        {
            _camera = Camera.main;

            _inputListener = inputListener;
            
            _inputListener.Init();

            _inputListener.TouchEnded += OnTouchEnded;

            _matchHandler = matchHandler;
        }

        private void OnTouchEnded(Vector3 touchPosition)
        {
            var worldPos = _camera.ScreenToWorldPoint(touchPosition);
            var hit = Physics2D.Raycast(worldPos, Vector2.zero);

            if (hit.collider != null && hit.collider.CompareTag("CellCollider"))
            {
                _matchHandler.CellTapped(hit.collider.gameObject.GetComponent<Cell>());
            }
        }

        public void Dispose()
        {
            _inputListener.TouchEnded -= OnTouchEnded;
        }
    }
}