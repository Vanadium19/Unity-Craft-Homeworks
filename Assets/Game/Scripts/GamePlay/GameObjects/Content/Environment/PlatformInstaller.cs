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
            Container.Bind<Platform>()
                .AsSingle()
                .NonLazy();
            
            Container.Bind<Transform>()
                .FromInstance(_transform)
                .AsSingle();

            Container.BindInterfacesAndSelfTo<TransformMoveComponent>()
                .AsSingle()
                .WithArguments(_speed);
        }
    }
}