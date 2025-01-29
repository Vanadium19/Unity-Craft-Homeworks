using UnityEngine;
using Zenject;

namespace Game.View.Installers
{
    public class EnemyViewInstaller : MonoInstaller
    {
        [SerializeField] private DamageView _damageView;

        public override void InstallBindings()
        {
            Container.Bind<DamageView>()
                .FromInstance(_damageView)
                .AsSingle();
        }
    }
}