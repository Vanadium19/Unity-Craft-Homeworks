using Game.Scripts.App.Repository;
using Game.Scripts.App.SaveLoad;
using Game.Scripts.App.SaveLoad.Serializers;
using Game.Scripts.App.SaveLoad.Serializers.ComponentSerializers;
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

            Container.BindInterfacesTo<CountdownSerializer>()
                .AsCached()
                .NonLazy();
            
            Container.BindInterfacesTo<DestinationPointSerializer>()
                .AsCached()
                .NonLazy();
            
            Container.BindInterfacesTo<HealthSerializer>()
                .AsCached()
                .NonLazy();
            
            Container.BindInterfacesTo<ResourceBagSerializer>()
                .AsCached()
                .NonLazy();
            
            Container.BindInterfacesTo<TeamSerializer>()
                .AsCached()
                .NonLazy();
            
            Container.BindInterfacesTo<EntityWorldSerializer>()
                .AsSingle()
                .NonLazy();
        }
    }
}