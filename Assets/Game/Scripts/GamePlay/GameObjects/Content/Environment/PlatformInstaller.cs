using Game.Core.Components;
using UnityEngine;
using Zenject;

namespace Game.Content.Environment
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

            Container.Bind<TransformMoveComponent>()
                .AsSingle()
                .WithArguments(_transform, _speed);
        }
    }
}