using SampleGame.App.SaveLoad;
using SampleGame.App.SaveLoad.Serializers;
using UnityEngine;
using Zenject;

namespace SampleGame.App
{
    [CreateAssetMenu(
        fileName = "GameSceneSavesInstaller",
        menuName = "Zenject/New GameScene Saves Installer"
    )]
    public class GameSceneSavesInstaller : ScriptableObjectInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesTo<GameSaveLoader>()
                .AsSingle()
                .NonLazy();
            
            ComponentSerializersInstaller.Install(Container);
            GameSerializersInstaller.Install(Container);
        }
    }
}