using Zenject;

namespace SampleGame.App.SaveLoad.Serializers
{
    public class GameSerializersInstaller : Installer<GameSerializersInstaller>
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesTo<EntityWorldSerializer>()
                .AsSingle()
                .NonLazy();
        }
    }
}