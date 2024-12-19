using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace Modules.UI
{
    public sealed class ParticleAnimator : MonoBehaviour
    {
        [SerializeField]
        private RectTransform _particlePrefab;

        [SerializeField]
        private Transform _parent;

        private readonly Queue<RectTransform> _queue = new();

        public void Emit(Vector3 from, Vector3 to, float duration = 1, Action onFinished = null)
        {
            if (_queue.TryDequeue(out RectTransform particle))
                particle.gameObject.SetActive(true);
            else
                particle = Instantiate(_particlePrefab, _parent);

            particle.transform.SetPositionAndRotation(from, Quaternion.identity);
            particle
                .DOMove(to, duration)
                .SetEase(Ease.OutExpo) 
                .OnComplete(() =>
                {
                    onFinished?.Invoke();
                    _queue.Enqueue(particle);
                    particle.gameObject.SetActive(false);
                });
        }
    }
}