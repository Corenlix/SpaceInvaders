using UnityEngine;

namespace Code.Gameplay.Spaceships
{
    public class FirePoint : MonoBehaviour
    {
        [SerializeField] private float _shootAngle;
        public float ShootAngle => _shootAngle;
    }
}