using System;
using DG.Tweening;
using UnityEngine;

namespace Game.Scripts.View
{
    public class DamageView : MonoBehaviour
    {
        [Header("Animation")]
        [SerializeField] private string _animationId;
        [SerializeField] private int _duration = 2;
        [SerializeField] private float _interval = 0.25f;
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private Color _color;

        [Header("Audio")]
        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private AudioClip _damageSound;

        private Color _startColor;

        private void Awake()
        {
            _startColor = _spriteRenderer.color;
        }

        public void TakeDamage()
        {
            if (string.IsNullOrEmpty(_animationId))
                throw new ArgumentException();

            DOTween.Kill(_animationId);
            _spriteRenderer.color = _startColor;
            _audioSource.PlayOneShot(_damageSound);

            _spriteRenderer.DOColor(_color, _interval)
                .SetLoops((int)(_duration / _interval), LoopType.Yoyo)
                .SetEase(Ease.Linear)
                .SetId(_animationId)
                .OnComplete(() => _spriteRenderer.color = _startColor);
        }
    }
}