using Game.Scripts.View;
using UnityEngine;
using Zenject;

namespace Game.Presenters
{
    public class EnemyPresenterInstaller : MonoInstaller
    {
        [SerializeField] private DamageView _damageView;

        public override void InstallBindings()
        {
            Container.BindInterfacesTo<DamagePresenter>()
                .AsSingle()
                .NonLazy();

            Container.Bind<DamageView>()
                .FromInstance(_damageView)
                .AsSingle();
        }
    }
}