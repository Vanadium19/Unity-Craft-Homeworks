using Game.Scripts.App.Repository;
using Game.Scripts.App.SaveLoad;
using Game.Scripts.App.SaveLoad.Serializers;
using Game.Scripts.App.SaveLoad.Serializers.ComponentSerializers;
using UnityEngine;
using Zenject;

namespace Game.Scripts.App.Installers
{
    [CreateAssetMenu(
        fileName = "AppInstaller",
        menuName = "Zenject/New App Installer"
    )]
    public class AppInstaller : ScriptableObjectInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesTo<GameRepository>()
                .AsSingle()
                .NonLazy();
            
            Container.BindInterfacesTo<GameSaveLoader>()
                .AsSingle()
                .NonLazy();

            ComponentSerializersInstaller.Install(Container);
            GameSerializersInstaller.Install(Container);
        }
    }
}