using Game.Items.Components.ExplodeComponent;

namespace Game.Items
{
    public class BombItem : BoosterItem
    {
        public override void InitComponents()
        {
            base.InitComponents();
            AddComponent<IItemExplodeComponent>
                (new ItemSquareExplodeComponent(1, Cell.Board, Cell.X,Cell.Y));
            
            
            
            
            
            
            // (int explosionRange, Board board, int x, int y) : base(board, x, y)
        }
    }
}