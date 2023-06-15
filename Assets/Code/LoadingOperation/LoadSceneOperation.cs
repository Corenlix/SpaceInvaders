using System;
using System.Threading.Tasks;
using UnityEngine.AddressableAssets;

namespace Code.LoadingOperation
{
    public class LoadSceneOperation : ILoadingOperation
    {
        private readonly AssetReference _scene;
        public string Description => "Load...";

        public LoadSceneOperation(AssetReference scene)
        {
            _scene = scene;
        }

        public async Task Load(Action<float> onProgress)
        {
            var loadOperation = Addressables.LoadSceneAsync(_scene);
            while (!loadOperation.IsDone)
            {
                onProgress(loadOperation.PercentComplete);
                await Task.Delay(1);
            }
        }
    }
}