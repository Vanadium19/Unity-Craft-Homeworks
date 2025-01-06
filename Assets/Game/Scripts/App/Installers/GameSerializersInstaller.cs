using Game.Scripts.App.SaveLoad.Serializers;
using Zenject;

namespace Game.Scripts.App.Installers
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