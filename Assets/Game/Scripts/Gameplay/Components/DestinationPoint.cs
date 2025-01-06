using UnityEngine;

namespace SampleGame.Gameplay
{
    //Can be extended
    public sealed class DestinationPoint : MonoBehaviour
    {
        ///Variable
        [field: SerializeField]
        public Vector3 Value { get; set; }
    }
}