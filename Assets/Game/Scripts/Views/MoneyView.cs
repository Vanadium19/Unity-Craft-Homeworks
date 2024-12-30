using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace Game.Views
{
    public class MoneyView : MonoBehaviour
    {
        private const string Id = "CoinAnimation";

        [SerializeField] private TMP_Text _money;
        [SerializeField] private Transform _coin;
        [SerializeField] private float _animationDelay = 2f;

        private readonly Queue<(int prevValue, int newValue)> _animationQueue = new();

        private bool _isAnimating;

        public Vector3 CoinPosition => _coin.position;

        public void SetMoney(int value)
        {
            DOTween.Kill(Id);
            _isAnimating = false;
            _animationQueue.Clear();

            _money.text = value.ToString();
        }
        
        public void AddMoney(int prevValue, int newValue)
        {
            if (prevValue >= newValue)
                return;

            _animationQueue.Enqueue((prevValue, newValue));

            if (!_isAnimating)
                StartNextAnimation();
        }

        private void StartNextAnimation()
        {
            if (_animationQueue.Count == 0)
            {
                _isAnimating = false;
                return;
            }

            var values = _animationQueue.Dequeue();

            _isAnimating = true;

            DOTween.To(() => values.prevValue,
                    value => { _money.text = value.ToString(); },
                    values.newValue,
                    _animationDelay)
                .SetEase(Ease.InOutSine)
                .OnComplete(StartNextAnimation)
                .SetId(Id);
        }
    }
}