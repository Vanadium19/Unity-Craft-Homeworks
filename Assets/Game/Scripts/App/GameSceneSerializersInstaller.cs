using SampleGame.App.SaveLoad;
using SampleGame.App.SaveLoad.Serializers;
using UnityEngine;
using Zenject;

namespace SampleGame.App
{
    [CreateAssetMenu(
        fileName = "GameSceneSerializersInstaller",
        menuName = "Zenject/New GameScene Serializers Installer"
    )]
    public class GameSceneSerializersInstaller : ScriptableObjectInstaller
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