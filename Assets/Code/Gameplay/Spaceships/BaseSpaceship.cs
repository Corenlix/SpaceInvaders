using UnityEngine;

namespace Code.Gameplay.Spaceships
{
    public abstract class BaseSpaceship : MonoBehaviour, ISpaceship
    {
        public abstract void Shoot();
        public void Destroy()
        {
            Destroy(gameObject);
        }
    }
}