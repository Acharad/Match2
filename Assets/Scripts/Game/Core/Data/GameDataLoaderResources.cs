using UnityEngine;

namespace Game.Core.Data
{
    public class GameDataLoaderResources : IGameDataLoader
    {
        public GameData LoadGameData()
        {
            return Resources.Load<GameData>("Data/GameData");
        }
    }
}