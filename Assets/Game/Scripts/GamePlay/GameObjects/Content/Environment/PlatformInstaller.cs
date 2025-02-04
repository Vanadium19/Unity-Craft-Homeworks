using Game.Core.Components;
using UnityEngine;
using Zenject;

namespace Game.Content.Environment
{
    public class PlatformInstaller : MonoInstaller
    {
        [SerializeField] private Transform _transform;
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
            Container.BindInterfacesAndSelfTo<TransformMoveComponent>()
                .AsSingle()
                .WithArguments(_speed);
        }
    }
}