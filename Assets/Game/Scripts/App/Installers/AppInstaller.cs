using Game.Scripts.App.SaveLoad;
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
            Container.BindInterfacesTo<GameSaveLoader>()
                .AsSingle()
                .NonLazy();

            RepositoryInstaller.Install(Container);
            ComponentSerializersInstaller.Install(Container);
            GameSerializersInstaller.Install(Container);
        }
    }
}