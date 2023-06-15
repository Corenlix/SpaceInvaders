using UnityEngine;
using Zenject;

namespace Code.UI.LoadingScreen
{
    public class LoadingScreenInstaller : MonoInstaller
    {
        [SerializeField] private LoadingScreen _loadingScreenPrefab;

        public override void InstallBindings()
        {
            Container.BindIFactory<LoadingScreen>()
                .FromMethod(container => container.InstantiatePrefabForComponent<LoadingScreen>(_loadingScreenPrefab));
        }
    }
}