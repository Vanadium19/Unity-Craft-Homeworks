using TMPro;
using UnityEngine;

namespace UI
{
    public class ScoreView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _score;
        
        public void SetScore(string score)
        {
            _score.text = score;
        }
    }
}