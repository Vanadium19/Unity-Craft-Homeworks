using Game.Core.Components;
using UnityEngine;
using Zenject;

namespace Game.Content.Environment
{
    public class PlatformInstaller : MonoInstaller
    {
        [SerializeField] private Transform _transform;
        [SerializeField] private Transform _startPoint;
        [SerializeField] private Transform _endPoint;
        [SerializeField] private float _speed = 2;

        public override void InstallBindings()
        {
            //Main
            Container.Bind<Platform>()
                .AsSingle()
                .NonLazy();

            //MonoBehaviors
            Container.Bind<Transform>()
                .FromInstance(_transform)
                .AsSingle();

            //Components
            Container.BindInterfacesAndSelfTo<PatrolComponent>()
                .AsSingle()
                .WithArguments(_startPoint.position, _endPoint.position, _speed);
        }
    }
}