using UnityEngine;
using UnityEngine.EventSystems;

namespace Modules.UI
{
    public sealed class HoverScaleAnimation : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField]
        public float scaleFactor = 1.2f; // Во сколько раз увеличить

        [SerializeField]
        public float animationSpeed = 10f; // Скорость анимации

        private Vector3 originalScale;
        private Vector3 targetScale;

        private void Start()
        {
            originalScale = this.transform.localScale;
            targetScale = originalScale;
        }

        private void Update()
        {
            this.transform.localScale = Vector3.Lerp(this.transform.localScale, targetScale, Time.deltaTime * animationSpeed);
        }
        
        public void OnPointerEnter(PointerEventData eventData)
        {
            targetScale = originalScale * scaleFactor; // Увеличиваем масштаб
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            targetScale = originalScale; // Возвращаем исходный масштаб
        }
    }
}