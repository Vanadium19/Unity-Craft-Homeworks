using UnityEngine;
using Zenject;

namespace Game.Presenters.Installers
{
    [CreateAssetMenu(
        fileName = "CharacterPresenterInstaller",
        menuName = "Zenject/New CharacterPresenterInstaller"
    )]
    public class CharacterPresenterInstaller : ScriptableObjectInstaller
    {
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
        }
    }
}