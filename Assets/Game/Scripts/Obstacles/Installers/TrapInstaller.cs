using Game.Components;
using Game.Obstacles.Enemies;
using UnityEngine;
using Zenject;

namespace Game.Obstacles.Installers
{
    public class TrapInstaller : MonoInstaller
    {
        [SerializeField] private Trap _trap;
        [SerializeField] private Rigidbody2D _rigidbody;
        [SerializeField] private UnityEventReceiver _unityEvents;

        [SerializeField] private int _damage = 1;
        [SerializeField] private int _health = 1;
        [SerializeField] private float _stunDelay = 0f;

        public override void InstallBindings()
        {
            Container.Bind<Trap>()
                .FromInstance(_trap)
                .AsSingle();

            Container.BindInterfacesAndSelfTo<PushableComponent>()
                .AsSingle()
                .WithArguments(_stunDelay);

            MonoBehaviorsInstaller.Install(Container, _rigidbody, _unityEvents);
            EnemyComponentsInstaller.Install(Container, _damage, _health);
        }
    }
}