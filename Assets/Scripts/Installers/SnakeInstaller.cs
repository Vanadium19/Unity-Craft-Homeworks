using Modules;
using PlayerInput;
using Player;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class SnakeInstaller : MonoInstaller
    {
        [SerializeField] private Snake _snake;
        
        public override void InstallBindings()
        {
            Container.BindInterfacesTo<Snake>()
                .FromInstance(_snake)
                .AsSingle();
            
            Container.BindInterfacesTo<MoveInput>()
                .AsSingle();
            
            Container.BindInterfacesTo<SnakeMoveController>()
                .AsSingle()
                .NonLazy();
        }
    }
}