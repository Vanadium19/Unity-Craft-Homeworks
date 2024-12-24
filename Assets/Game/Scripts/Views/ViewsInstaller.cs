using UnityEngine;
using Zenject;

namespace Game.Views
{
    public sealed class ViewsInstaller : MonoInstaller
    {
        [SerializeField] private PlanetPopup _planetPopup;
        
        public override void InstallBindings()
        {
            Container.Bind<PlanetPopup>()
                .FromInstance(_planetPopup)
                .AsSingle();
        }
    }
}