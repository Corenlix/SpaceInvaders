using System.Collections.Generic;
using Code.LoadingOperation;
using Code.UI.LoadingScreen;
using UnityEngine.SceneManagement;
using Zenject;

namespace Code.Levels
{
    public class LevelLoader : ILevelLoader, IInitializable
    {
        private readonly LevelData[] _levels;
        private readonly IFactory<LoadingScreen> _loadingScreenFactory;
        private int _currentLevel = -1;

        public LevelLoader(LevelData[] levels, IFactory<LoadingScreen> loadingScreenFactory)
        {
            _levels = levels;
            _loadingScreenFactory = loadingScreenFactory;
        }
        
        public void LoadNext()
        {
            _currentLevel = (_currentLevel + 1) % _levels.Length;
            LoadLevel(_levels[_currentLevel]);
        }

        public void RestartCurrent()
        {
            LoadLevel(_levels[_currentLevel]);
        }

        private void LoadLevel(LevelData levelData)
        {
            var screen = _loadingScreenFactory.Create();
            var operations = new Queue<ILoadingOperation>();
            operations.Enqueue(new LoadSceneOperation(levelData.Scene));
            screen.Load(operations);
        }

        public void Initialize()
        {
            LoadNext();
        }
    }
}