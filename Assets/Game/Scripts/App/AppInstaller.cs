using Game.Scripts.App.Repository;
using Game.Scripts.App.SaveLoad;
using Game.Scripts.App.SaveLoad.Serializers;
using Modules.Entities;
using UnityEngine;
using Zenject;

namespace Game.Scripts.App
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
            
            Container.Bind<EntitySerializer>()
                .AsSingle()
                .NonLazy();
            
            Container.BindInterfacesTo<EntitiesSerializer>()
                .AsSingle()
                .NonLazy();
        }
    }
}