using Modules.Entities;
using UnityEngine;

namespace SampleGame.Gameplay
{
    //Can be extended
    public sealed class TargetObject : MonoBehaviour
    {
        ///Variable
        [field: SerializeField]
        public Entity Value { get; set; }
    }
}