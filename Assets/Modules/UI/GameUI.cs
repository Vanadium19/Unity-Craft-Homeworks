using TMPro;
using UnityEngine;

namespace SnakeGame
{
    //Don't Modify!
    public sealed class GameUI : MonoBehaviour, IGameUI
    {
        [SerializeField]
        private GameObject _winScreen;

        [SerializeField]
        private GameObject _loseScreen;

        [SerializeField]
        private TMP_Text _difficultyText;

        [SerializeField]
        private TMP_Text _score;

        private void Awake()
        {
            _winScreen.SetActive(false);
            _loseScreen.SetActive(false);
        }

        public void SetDifficulty(int current, int max)
        {
            _difficultyText.text = $"Level: {current}/{max}";
        }

        public void SetScore(string score)
        {
            _score.text = $"Score: {score}";
        }

        public void GameOver(bool win)
        {
            _winScreen.SetActive(win);
            _loseScreen.SetActive(!win);
        }
    }
}