using System;
using DG.Tweening;
using UnityEngine;

namespace Game.Scripts.View
{
    public class DamageView : MonoBehaviour
    {
        [Header("Animation")]
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
            _spriteRenderer.color = _startColor;

            if (_audioSource != null)
                _audioSource.PlayOneShot(_damageSound);

            _spriteRenderer.DOColor(_color, _interval)
                .SetLoops((int)(_duration / _interval), LoopType.Yoyo)
                .SetEase(Ease.Linear)
                .OnComplete(() => _spriteRenderer.color = _startColor);
        }
    }
}