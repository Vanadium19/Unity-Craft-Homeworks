using System;
using UnityEngine;

namespace Modules.Utils
{
    [Serializable]
    public sealed class Countdown
    {
        [field: SerializeField]
        public float RemainingTime { get; private set; }
        
        [field: SerializeField]
        public float Duration { get; private set; }

        public Countdown() { }
        
        public Countdown(float duration)
        {
            RemainingTime = duration;
            Duration = duration;
        }
        
        public void Tick(float deltaTime)
        {
            RemainingTime -= deltaTime;
        }

        public void Reset()
        {
            RemainingTime = Duration;
        }

        public bool IsPlaying()
        {
            return RemainingTime > 0;
        }
    }
}