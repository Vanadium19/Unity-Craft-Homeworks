using Game.Scripts.View;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.TextCore.Text;
using Zenject;

namespace Game.Presenters
{
    public class CharacterPresenterInstaller : MonoInstaller
    {
        [SerializeField] private PushView _pushView;
        [SerializeField] private JumpView _jumpView;
        [SerializeField] private DamageView _damageView;

        public override void InstallBindings()
        {
            Container.BindInterfacesTo<DamagePresenter>()
                .AsSingle()
                .NonLazy();

            Container.BindInterfacesTo<JumpPresenter>()
                .AsSingle()
                .NonLazy();

            Container.BindInterfacesTo<PushPresenter>()
                .AsSingle()
                .NonLazy();

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