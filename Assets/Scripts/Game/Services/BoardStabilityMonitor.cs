using System;
using System.Collections.Generic;
using Game.Core.Board;
using Game.Core.Item;

namespace Game.Services
{
    public class BoardStabilityMonitor : IDisposable
    {
        private HashSet<ItemBase> _unstableItems = new();
        
        private Board _board;

        private BoardState CurrentState
        {
            get => _board.State;
            set
            {
                _board.State = value;
                OnStateChanged?.Invoke(value);
            }
        }

        private ItemFactory _itemFactory;
        

        public event Action<BoardState> OnStateChanged;
        
        public void Init(ItemFactory itemFactory, Board board)
        {
            _itemFactory = itemFactory;
            _board = board;

            _itemFactory.OnItemCreated += OnItemCreated;
        }

        private void OnItemCreated(ItemBase itemBase)
        {
            if (itemBase == null) return;
            var fallComponent = itemBase.GetFallComponent();
            
            if (fallComponent == null) return;
            fallComponent.OnStartFalling += HandleItemStartFalling;
            fallComponent.OnFinishFalling += HandleItemFinishFalling;
            itemBase.OnItemDestroyed += OnItemDestroyed;
        }

        private void OnItemDestroyed(ItemBase itemBase)
        {
            if (itemBase == null) return;
            
            if (_unstableItems.Contains(itemBase))
            {
                _unstableItems.Remove(itemBase);
            }
            
            var fallComponent = itemBase.GetFallComponent();
            
            if (fallComponent == null) return;
            
            fallComponent.OnStartFalling -= HandleItemStartFalling;
            fallComponent.OnFinishFalling -= HandleItemFinishFalling;
            itemBase.OnItemDestroyed -= OnItemDestroyed;
        }

        private void HandleItemStartFalling(ItemBase itemBase)
        {
            _unstableItems.Add(itemBase);
            CurrentState = BoardState.Unstable;
        }
        
        private void HandleItemFinishFalling(ItemBase itemBase)
        {
            _unstableItems.Remove(itemBase);

            if (_unstableItems.Count <= 0)
            {
                CurrentState = BoardState.Stable; 
            }
        }

        public void Dispose()
        {
            _itemFactory.OnItemCreated -= OnItemCreated;
        }
    }
}