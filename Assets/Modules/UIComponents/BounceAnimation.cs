using UnityEngine;

namespace Modules.UI
{
    public class BounceAnimation : MonoBehaviour
    {
        public RectTransform rectTransform; // RectTransform попапа
        public float animationDuration = 0.5f; // Длительность анимации (в секундах)
        public Vector3 hiddenScale = Vector3.one; // Начальный масштаб
        public Vector3 shownScale = Vector3.one; // Конечный масштаб
        public float bounceIntensity = 1.2f; // Интенсивность "отскока" (чуть больше 1)
        
        private void OnEnable()
        {
            StartCoroutine(BounceInAnimation());
        }

        private System.Collections.IEnumerator BounceInAnimation()
        {
            rectTransform.localScale = hiddenScale;

            float elapsedTime = 0f;
            Vector3 overshootScale = shownScale * bounceIntensity;

            // Увеличиваем до overshootScale
            while (elapsedTime < animationDuration * 0.5f)
            {
                elapsedTime += Time.deltaTime;
                rectTransform.localScale = Vector3.Lerp(hiddenScale, overshootScale, elapsedTime / (animationDuration * 0.5f));
                yield return null;
            }

            elapsedTime = 0f;

            // Уменьшаем до конечного shownScale
            while (elapsedTime < animationDuration * 0.5f)
            {
                elapsedTime += Time.deltaTime;
                rectTransform.localScale = Vector3.Lerp(overshootScale, shownScale, elapsedTime / (animationDuration * 0.5f));
                yield return null;
            }

            rectTransform.localScale = shownScale;
        }
    }
}