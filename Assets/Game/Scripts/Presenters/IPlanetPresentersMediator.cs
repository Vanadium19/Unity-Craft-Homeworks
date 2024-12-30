using UnityEngine;

namespace Game.Presenters
{
    public interface IPlanetPresentersMediator
    {
        public void GatherIncome(Vector3 planetPosition, int range);
    }
}