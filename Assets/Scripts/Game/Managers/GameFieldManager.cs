using System;
using Game.Core.Board;
using Game.Core.Data;
using Game.Core.Item;
using Game.Core.Level;
using Services.Input;
using UnityEngine;
using Game.Services;
using Game.Services.Lock;
using Game.Services.Pool;

namespace Game.Managers
{
    public class GameFieldManager : MonoBehaviour
    {
        [SerializeField] private int levelIndex = 1;
        [SerializeField] private Board Board;
        [SerializeField] private InputListener InputListener;
        
        private LevelData _levelData;
        private ItemFactory _itemFactory;
        private GameData _gameData;

        private readonly MatchHandler _matchHandler = new();
        private readonly MatchFinder _matchFinder = new();
        private readonly GravityHandler _gravityHandler = new();
        private readonly BoardSpawner _boardSpawner = new();
        private readonly HintHighlighter _hintHighlighter = new();
        private readonly DeadlockDetector _deadlockDetector = new();
        private readonly BoardStabilityMonitor _boardStabilityMonitor = new();
        private readonly ItemPoolCreator _itemPoolCreator = new();
        private readonly BoardShuffler _boardShuffler = new();
        
        private void Start()
        {
            LoadGameData();
            InitializeItemFactory();
            InitializeBoard();
            InitializeServices();
            InitializeInput();
        }

        private void LoadGameData()
        {
            _gameData = new GameDataLoaderResources().LoadGameData();
            _levelData = new LevelLoaderResources().LoadLevel(levelIndex);
        }

        private void InitializeItemFactory()
        {
            var levelDataLoader = new ItemDataLoaderResources();
            _itemPoolCreator.Init(_levelData, levelDataLoader);
            _itemFactory = new ItemFactory(levelDataLoader, _itemPoolCreator);
            _boardStabilityMonitor.Init(_itemFactory, Board);
        }

        private void InitializeBoard()
        {
            Board.Init(_levelData, _itemFactory);
        }

        private void InitializeServices()
        {
            _matchHandler.Init(_matchFinder, _gameData.MinMatchCount);
            _hintHighlighter.Init(Board, _matchFinder, _gameData.HintThresholds);
            _deadlockDetector.Init(Board, _matchFinder, _boardStabilityMonitor, _gameData.MinMatchCount);
            _gravityHandler.Init(Board);
            _boardSpawner.Init(Board, _itemFactory, _levelData.GeneratorData);
            _boardShuffler.Init(Board,_deadlockDetector, _gameData.MinMatchCount);
        }

        private void InitializeInput()
        {
            var inputManager = new InputManager();
            inputManager.Init(InputListener, _matchHandler);
        }

        private void Update()
        {
            _boardSpawner.SpawnItems();
            _gravityHandler.ApplyGravity();
            _hintHighlighter.ShowHints();
        }

        private void OnDestroy()
        {
            _boardShuffler.Dispose();
            _boardStabilityMonitor.Dispose();
            _deadlockDetector.Dispose();
        }
    }
}
