using DG.Tweening;
using UnityEngine;

namespace Game.Scripts.View
{
    public class JumpView : MonoBehaviour
    {
        [Header("Animation")]
        [SerializeField] private float _duration;
        [SerializeField] private int _loopCount;
        [SerializeField] private Vector3 _targetScale;

        [Header("Sound")]
        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private AudioClip _jumpSound;
        
        private Transform _transform;

        private void Awake()
        {
            _transform = transform;
        }

        public void Jump()
        {
            _transform.DOScale(_targetScale, _duration).SetEase(Ease.OutBounce).SetLoops(_loopCount, LoopType.Yoyo);
        }
    }
}