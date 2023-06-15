using UnityEngine;

namespace Code.Gameplay.Camera
{
    public class CameraFollower : MonoBehaviour, ICameraFollower
    {
        [SerializeField] private UnityEngine.Camera _camera;
    
        public UnityEngine.Camera Camera => _camera;
    }
}