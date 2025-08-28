namespace Game.Core.Level
{
    public interface ILevelLoader
    {
        public LevelData LoadLevel(int levelNumber);
    }
}