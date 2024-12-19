using DG.Tweening;
using UnityEngine;

namespace Modules.UI
{
    public sealed class FloatingAnimation : MonoBehaviour
    {
        [SerializeField] private float floatDistance = 0.5f; // Расстояние покачивания
        [SerializeField] private float duration = 2f; // Длительность подъема и опускания

        private void Start()
        {
            StartFloating();
        }

        private void StartFloating()
        {
            // Начальная позиция объекта
            Vector3 originalPosition = transform.position;

            // Анимация подъема
            transform
                .DOMoveY(originalPosition.y + floatDistance, duration)
                .SetEase(Ease.InOutSine) // Плавный переход
                .SetLoops(-1, LoopType.Yoyo); // Бесконечный цикл вверх-вниз
        }
    }
}