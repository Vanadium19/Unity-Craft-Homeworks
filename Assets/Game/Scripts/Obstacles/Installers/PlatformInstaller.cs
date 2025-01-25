using Game.Components;
using Game.Obstacles.Environment;
using UnityEngine;
using Zenject;

namespace Game.Obstacles.Installers
{
    public class PlatformInstaller : MonoInstaller
    {
        [SerializeField] private Platform _platform;
        [SerializeField] private Transform _transform;

        [Header("Move Controller")]
        [SerializeField] private Transform _startPoint;
        [SerializeField] private Transform _endPoint;

        [Header("Main Settings")]
        [SerializeField] private float _speed = 2;
        
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<Platform>()
                .FromInstance(_platform)
                .AsSingle();

            Container.BindInterfacesTo<PointsMoveController>()
                .AsCached()
                .WithArguments(_startPoint.position, _endPoint.position)
                .NonLazy();

            Container.Bind<TransformMover>()
                .AsSingle()
                .WithArguments(_transform, _speed);
        }
    }
}