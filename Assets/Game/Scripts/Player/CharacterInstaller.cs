using Game.Components;
using UnityEngine;
using Zenject;

namespace Game.Player
{
    public class CharacterInstaller : MonoInstaller
    {
        [SerializeField] private Character _character;
        [SerializeField] private Rigidbody2D _rigidbody;
        [SerializeField] private Transform _transform;

        // [Header("Player Settings")]
        [SerializeField] private int _health = 5;
        [SerializeField] private float _speed = 3f;
        [SerializeField] private float _jumpForce = 5f;
        [SerializeField] private float _jumpDelay = 0.5f;


        public override void InstallBindings()
        {
            Container.BindInterfacesTo<Character>()
                .FromInstance(_character)
                .AsSingle();

            //Controllers
            ControllersInstaller.Install(Container);

            //Components
            Container.Bind<Rigidbody2D>()
                .FromInstance(_rigidbody)
                .AsSingle();

            StateComponentsInstaller.Install(Container, _health);
            MoveComponentsInstaller.Install(Container, _transform, _jumpForce, _jumpDelay, _speed);
        }
    }
}