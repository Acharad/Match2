using UnityEngine;

namespace Match3.Items
{
    public class ItemView : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer spriteRenderer;
        public ItemData Data { get; private set; }
        
        protected bool CanFall;
        
        private Cell _cell;


        public Cell Cell
        {
            get { return _cell; }
            set
            {
                if (_cell == value) return;

                var oldCell = _cell;
                _cell = value;

                if (oldCell != null && oldCell.Item == this)
                {
                    oldCell.Item = null;
                }

                if (value != null)
                {
                    value.Item = this;
                    gameObject.name = _cell.gameObject.name + " " + GetType().Name;
                }

            }
        }
        
        public virtual void Prepare(ItemData itemData)
        {
            Data = itemData;
        }

        public virtual void Hit(HitData data)
        {
            
        }
    }
}
