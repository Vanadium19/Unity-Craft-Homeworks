using UnityEngine;
using Zenject;

namespace Game.View.Installers
{
    public class CharacterViewInstaller : MonoInstaller
    {
        [SerializeField] private PushView _pushView;
        [SerializeField] private JumpView _jumpView;
        [SerializeField] private DamageView _damageView;
        
        public override void InstallBindings()
        {
            Container.Bind<PushView>()
                .FromInstance(_pushView)
                .AsSingle();

            Container.Bind<DamageView>()
                .FromInstance(_damageView)
                .AsSingle();

            Container.Bind<JumpView>()
                .FromInstance(_jumpView)
                .AsSingle();
        }
    }
}