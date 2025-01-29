using Game.Components;
using UnityEngine;
using Zenject;

namespace Game.Obstacles.Installers
{
    public class MonoBehaviorsInstaller : Installer<Rigidbody2D, UnityEventReceiver, MonoBehaviorsInstaller>
    {
        private readonly Rigidbody2D _rigidbody;
        private readonly UnityEventReceiver _unityEvents;

        public MonoBehaviorsInstaller(Rigidbody2D rigidbody, UnityEventReceiver unityEvents)
        {
            _rigidbody = rigidbody;
            _unityEvents = unityEvents;
        }

        public override void InstallBindings()
        {
            Container.Bind<UnityEventReceiver>()
                .FromInstance(_unityEvents)
                .AsSingle();

            Container.Bind<Rigidbody2D>()
                .FromInstance(_rigidbody)
                .AsSingle();
        }
    }
}