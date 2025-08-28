using System;
using System.Collections.Generic;
using Game.Items.Components;
using UnityEngine;

namespace Game.Core.Item
{
    public class ItemBase : MonoBehaviour, IPooledItem
    {
        [SerializeField] private SpriteRenderer SpriteRenderer;
        [SerializeField] protected ItemFallComponent itemFallComponent;
        public ItemShufflerComponent ItemShufflerComponent;
        
        protected ItemData _itemData;
        protected ItemUIData _itemUIData;
        
        private int _defaultSortingOrder = 0;
        
        protected bool CanFall = true;
        protected bool CanTapped = false;

        public bool CanExplodeWithTab => CanTapped && !itemFallComponent.IsFalling;
        public bool IsAvailableForMatch => !itemFallComponent.IsFalling;
        
        public event Action<ItemBase> OnItemDestroyed;
        
        private Cell _cell;
        public Cell Cell
        {
            get { return _cell; }
            set
            {
                if (_cell == value) return;

                var oldCell = _cell;
                _cell = value;

                if (oldCell != null && oldCell.ItemBase == this)
                {
                    oldCell.ItemBase = null;
                }

                if (value != null)
                {
                    
                    value.ItemBase = this;
                    SpriteRenderer.sortingOrder = _cell.Y + _defaultSortingOrder;
                }
            }
        }
        
        public virtual void Init(ItemData itemData, ItemUIData uiData)
        {
            _itemData = itemData;
            _itemUIData = uiData;
        }
        
        protected void ChangeSprite(int index)
        {
            if (index < 0 || index > _itemUIData.ItemSprites.Count - 1)
            {
                Debug.LogException(new IndexOutOfRangeException
                    ($"{nameof(ItemBase)} | Index {index} is out of range of ItemUIData sprites."));
                return;
            }
            
            SpriteRenderer.sprite = _itemUIData.ItemSprites[index];
        }

        public virtual ItemColor GetItemColor()
        {
            return _itemData.Color;
        }
        
        public void TryFall()
        {
            if (!CanFall) return;
            itemFallComponent.FallTo(Cell.GetBottomAvailableCell());
        }
        
        public virtual void TryExecute()
        {
            DestroyItem();
        }
        
        public virtual void TryExecuteByNearMatch() { }
        
        public virtual void ShowHint(int groupCount, List<int> HintThresholds) {}

        public ItemFallComponent GetFallComponent()
        {
            return itemFallComponent;
        }
        
        protected void DestroyItem()
        {
            Cell.ItemBase = null;
            Cell = null;
            
            OnItemDestroyed?.Invoke(this);
        }
    }
}