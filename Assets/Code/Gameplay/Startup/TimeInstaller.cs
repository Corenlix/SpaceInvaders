using Railcar.Time;
using Railcar.Time.Mono;
using UnityEngine;
using Zenject;

namespace Code.Gameplay.Startup
{
    public class TimeInstaller : MonoInstaller
    {
        [SerializeField] private PausableTimeWrapper _timeLord;

        public override void InstallBindings()
        {
            Container.Bind<ITimeLord>()
                .FromInstance(_timeLord)
                .AsSingle();

            Container.Bind<ITimeObservable>()
                .WithId(TimeID.Fixed)
                .FromInstance(_timeLord.FixedTime);
            Container.Bind<ITimeObservable>()
                .WithId(TimeID.Frame)
                .FromInstance(_timeLord.FrameTime);
            Container.Bind<ITimeObservable>()
                .WithId(TimeID.LateFrame)
                .FromInstance(_timeLord.LateFrameTime);
        }
    }
}