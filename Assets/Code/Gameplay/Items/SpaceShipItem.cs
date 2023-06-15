using Code.Gameplay.Spaceships;
using UnityEngine;
using Zenject;

namespace Code.Gameplay.Items
{
    public class SpaceShipItem : Item
    {
        [SerializeField] private SpaceshipConfig _spaceshipConfig;
        private SpaceshipContainer _shipContainer;

        [Inject]
        private void Inject(SpaceshipContainer spaceshipContainer)
        {
            _shipContainer = spaceshipContainer;
        }

        protected override void OnPicked()
        {
            _shipContainer.EquipShip(_spaceshipConfig);
        }
    }
}