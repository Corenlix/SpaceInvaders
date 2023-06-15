using System.Collections.Generic;
using System.Threading.Tasks;
using Code.LoadingOperation;
using UnityEngine;

namespace Code.UI.LoadingScreen
{
    public abstract class LoadingScreen : MonoBehaviour
    {
        public async void Load(Queue<ILoadingOperation> loadingOperations)
        {
            OnStartLoad();
            
            foreach (var operation in loadingOperations)
            {
                OnNextOperation(operation);
                await operation.Load(OnProgress);
                await WaitForFullLoadedView();
            }
            
            OnFinishLoad();
        }

        private async Task WaitForFullLoadedView()
        {
            OnProgress(1);
            await Task.Delay(150);
        }

        protected abstract void OnStartLoad();
        protected abstract void OnNextOperation(ILoadingOperation operation);
        protected abstract void OnProgress(float progress);
        protected abstract void OnFinishLoad();
    }
}