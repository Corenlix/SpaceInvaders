using UniRx;

namespace Code.Gameplay.Score
{
    public interface IScoreStorage
    {
        IReadOnlyReactiveProperty<int> Score { get; }
        void Obtain(int amount);
    }
}