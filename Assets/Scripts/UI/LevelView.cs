using TMPro;
using UnityEngine;

namespace UI
{
    public class LevelView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _score;

        public void SetLevel(string level)
        {
            _score.text = level;
        }
    }
}