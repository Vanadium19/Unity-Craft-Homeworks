using UnityEngine;
using Zenject;

namespace Game.Controllers.Installers
{
    [CreateAssetMenu(
        fileName = "PlayerControllersInstaller",
        menuName = "Zenject/New PlayerControllersInstaller"
    )]
    public class PlayerControllersInstaller : ScriptableObjectInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesTo<MoveController>()
                .AsSingle()
                .NonLazy();

            Container.BindInterfacesTo<JumpController>()
                .AsSingle()
                .NonLazy();
            
            Container.BindInterfacesTo<PushController>()
                .AsSingle()
                .NonLazy();
        }
    }
}