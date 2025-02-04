using Game.Core.Components;
using UnityEngine;
using Zenject;

namespace Game.GameSytems.Controllers
{
    public class PushController : ITickable
    {
        private readonly IPlayerPusher _character;

        public PushController(IPlayerPusher character)
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