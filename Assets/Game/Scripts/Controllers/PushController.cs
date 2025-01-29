using Game.Components.Interfaces;
using UnityEngine;
using Zenject;

namespace Game.Controllers
{
    public class PushController : ITickable
    {
        private readonly IPusher _character;

        public PushController(IPusher character)
        {
            _character = character;
        }

        public void Tick()
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
                _character.Push();
            else if (Input.GetKeyDown(KeyCode.Mouse1))
                _character.Toss();
        }
    }
}