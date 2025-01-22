using Game.Components;
using UnityEngine;
using Zenject;

namespace Game.Player
{
    public class CharacterInstaller : MonoInstaller
    {
        [SerializeField] private Character _character;
        [SerializeField] private Rigidbody2D _rigidbody;

        [Header("Player Settings")]
        [SerializeField] private int _health = 5;
        [SerializeField] private float _speed = 3f;
        [SerializeField] private float _jumpForce = 5f;


        public override void InstallBindings()
        {
            Container.BindInterfacesTo<Character>()
                .FromInstance(_character)
                .AsSingle();

            //Controllers
            ControllersInstaller.Install(Container);

            //Components
            ComponentsInstaller.Install(Container, _rigidbody, _speed, _jumpForce, _health);
        }
    }
}