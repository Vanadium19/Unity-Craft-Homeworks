using Modules.Money;
using Zenject;

namespace Game.Gameplay
{
    //Don't modify
    public sealed class MoneyInstaller : Installer<int, MoneyInstaller>
    {
        [Inject]
        private int _initialMoney;
        
        public override void InstallBindings()
        {
            this.Container
                .BindInterfacesAndSelfTo<MoneyStorage>()
                .AsSingle()
                .WithArguments(_initialMoney)
                .NonLazy();

            this.Container
                .BindInterfacesTo<MoneyAdapter>()
                .AsSingle()
                .NonLazy();
        }
    }
}