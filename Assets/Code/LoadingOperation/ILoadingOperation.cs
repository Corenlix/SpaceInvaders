using System;
using System.Threading.Tasks;

namespace Code.LoadingOperation
{
    public interface ILoadingOperation
    {
        string Description { get; }
        Task Load(Action<float> onProgress);
    }
}