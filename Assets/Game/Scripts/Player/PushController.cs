using Game.Components;
using UnityEngine;
using Zenject;

namespace Game.Player
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