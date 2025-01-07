using Game.Scripts.App.SaveLoad.Serializers.ComponentSerializers;
using Zenject;

namespace Game.Scripts.App.Installers
{
    public class ComponentSerializersInstaller : Installer<ComponentSerializersInstaller>
    {
        public override void InstallBindings()
        {
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

            Container.BindInterfacesTo<TargetObjectSerializer>()
                .AsCached()
                .NonLazy();
        }
    }
}