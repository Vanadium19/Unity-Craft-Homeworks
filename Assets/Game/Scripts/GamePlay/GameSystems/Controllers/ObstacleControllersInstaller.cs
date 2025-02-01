using UnityEngine;
using Zenject;

namespace Game.GameSytems.Controllers
{
    public class ObstacleControllersInstaller : MonoInstaller
    {
        [SerializeField] private Transform _startPoint;
        [SerializeField] private Transform _endPoint;
        
        public override void InstallBindings()
        {
            Container.BindInterfacesTo<PointsMoveController>()
                .AsCached()
                .WithArguments(_startPoint.position, _endPoint.position)
                .NonLazy();
        }
    }
}