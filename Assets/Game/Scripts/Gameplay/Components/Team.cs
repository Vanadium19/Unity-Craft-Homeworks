using SampleGame.Common;
using UnityEngine;

namespace SampleGame.Gameplay
{
    //Can be extended
    public sealed class Team : MonoBehaviour
    {
        ///Variable
        [field: SerializeField]
        public TeamType Type { get; set; }
    }
}