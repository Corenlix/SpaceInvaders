using UnityEngine;
using Zenject;

namespace Code.Gameplay.Camera
{
    public class CameraInstaller : MonoInstaller
    {
        [SerializeField] private CameraFollower _cameraFollower;

        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<ICameraFollower>().FromInstance(_cameraFollower).AsSingle();
        }
    }
}