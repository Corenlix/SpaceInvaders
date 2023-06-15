using Railcar.Time;
using UniRx;
using UnityEngine;
using Zenject;

namespace Code.Gameplay.Spaceships
{
    public class SpaceshipContainer : MonoBehaviour, IInitializable
    {
        [SerializeField] private SpaceshipConfig _initialShip;
        private readonly ReactiveProperty<ISpaceship> _currentShip = new();
        private IFactory<Transform, SpaceshipConfig, ISpaceship> _factory;

        public IReadOnlyReactiveProperty<ISpaceship> CurrentShip => _currentShip;

        [Inject]
        private void Inject(IFactory<Transform, SpaceshipConfig, ISpaceship> factory)
        {
            _factory = factory;
        }

        public void Initialize()
        {
            EquipShip(_initialShip);
        }

        public void EquipShip(SpaceshipConfig spaceshipConfig)
        {
            _currentShip.Value?.Destroy();
            _currentShip.Value = _factory.Create(transform, spaceshipConfig);
        }
    }
}