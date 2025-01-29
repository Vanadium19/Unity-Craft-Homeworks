using UnityEngine;
using Zenject;

namespace Game.Presenters.Installers
{
    [CreateAssetMenu(
        fileName = "EnemyPresenterInstaller",
        menuName = "Zenject/New EnemyPresenterInstaller"
    )]
    public class EnemyPresenterInstaller : ScriptableObjectInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesTo<DamagePresenter>()
                .AsSingle()
                .NonLazy();
        }
    }
}