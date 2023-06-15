using UniRx;

namespace Code.Gameplay.Score
{
    public class ScoreStorage : IScoreStorage
    {
        private readonly ReactiveProperty<int> _score = new();

        public IReadOnlyReactiveProperty<int> Score => _score;

        public void Obtain(int amount)
        {
            _score.Value += amount;
        }
    }
}