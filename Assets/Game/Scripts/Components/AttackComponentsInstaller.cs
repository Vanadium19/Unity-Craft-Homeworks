using Zenject;

namespace Game.Components
{
    public class AttackComponentsInstaller : Installer<PushParams, AttackComponentsInstaller>
    {
        private readonly PushParams _pushParams;

        public AttackComponentsInstaller(PushParams pushParams)
        {
            _pushParams = pushParams;
        }

        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<Pusher>()
                .AsSingle()
                .WithArguments(_pushParams);
        }
    }
}