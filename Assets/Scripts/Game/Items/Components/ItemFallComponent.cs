using System;
using Game.Core.Item;
using UnityEngine;

namespace Game.Items.Components
{
    public class ItemFallComponent : MonoBehaviour
    {
        private Cell _targetCell;
        private ItemBase _itemBase;
        
        private static float _startVel = 0F;
        private static float _acc = 0.4F;
        private static float _maxSpeed = 20F;

        private float _vel = _startVel;

        private Vector3 _targetPosition;
        private bool _isFalling;
        public bool IsFalling
        {
            get => _isFalling;
            private set
            {
                _isFalling = value;
                if (value)
                    OnStartFalling?.Invoke(_itemBase);
                else
                    OnFinishFalling?.Invoke(_itemBase);
            }
        }

        private bool _isAnimatingFall = false;

        public event Action<ItemBase> OnStartFalling;
        public event Action<ItemBase> OnFinishFalling;

        public void SetItem(ItemBase itemBase)
        {
            _itemBase = itemBase;
        }
        
        public void FallTo(Cell targetCell)
        {
            if (_targetCell != null && targetCell.Y >= _targetCell.Y) return;
            _targetCell = targetCell;
            _itemBase.Cell = _targetCell;
            _targetPosition = _targetCell.transform.position;
            IsFalling = true;
            _isAnimatingFall = true;
        }
        
        private void Update()
        {
            if (!_isAnimatingFall) return;
            _vel += _acc;
            _vel = _vel >= _maxSpeed ? _maxSpeed : _vel;
            var p = _itemBase.transform.position;
            p.y -= _vel * Time.deltaTime;
            if (p.y <= _targetPosition.y)
            {
                _isAnimatingFall = false;
                _targetCell = null;
                p.y = _targetPosition.y;
                _vel = _startVel;
            }

            _itemBase.transform.position = p;
            if (_isAnimatingFall == false)
                IsFalling = false;
        }
    }
}