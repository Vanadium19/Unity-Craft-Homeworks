using SampleGame.App.Repository;
using SampleGame.App.SaveLoad;
using SampleGame.App.SaveLoad.Serializers;
using UnityEngine;
using Zenject;

namespace SampleGame.App
{
    [CreateAssetMenu(
        fileName = "AppInstaller",
        menuName = "Zenject/New App Installer"
    )]
    public class AppInstaller : ScriptableObjectInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesTo<GameSaveLoader>()
                .AsSingle()
                .NonLazy();

            RepositoryInstaller.Install(Container);
            ComponentSerializersInstaller.Install(Container);
            GameSerializersInstaller.Install(Container);
        }
    }
}