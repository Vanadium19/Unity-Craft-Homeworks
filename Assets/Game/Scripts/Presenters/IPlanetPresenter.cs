using System;
using Modules.Planets;
using UnityEngine;

namespace Game.Presenters
{
    public interface IPlanetPresenter : IPresenter
    {
        public event Action<IPlanet> PopupOpened;
        public event Action<Vector3> IncomeGathered;
    }
}