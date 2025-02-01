using Game.Components;
using Game.Obstacles.Environment;
using Game.Presenters;
using UnityEngine;
using Zenject;

namespace Game.Obstacles.Installers
{
    public class PlatformInstaller : MonoInstaller
    {
        [SerializeField] private Platform _platform;
        [SerializeField] private Transform _transform;
        [SerializeField] private float _speed = 2;
        
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<Platform>()
                .FromInstance(_platform)
                .AsSingle();

            Container.Bind<TransformMover>()
                .AsSingle()
                .WithArguments(_transform, _speed);
        }
    }
}