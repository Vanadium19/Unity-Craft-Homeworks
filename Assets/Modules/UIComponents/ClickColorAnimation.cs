using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Modules.UI
{
    public class ClickColorAnimation : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        public Image targetImage; // UI-элемент, цвет которого нужно менять
        public float animationSpeed = 10; // Длительность анимации (в секундах)

        public Color clickedColor = Color.gray; // Цвет при нажатии
        private Color originalColor; // Исходный цвет
        private Color targetColor; // Таргет цвет
        
        private void Start()
        {
            originalColor = targetImage.color;
            targetColor = originalColor;
        }
        
        private void Update()
        {
            targetImage.color = Color.Lerp(targetImage.color, targetColor, Time.deltaTime * animationSpeed);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            targetColor = clickedColor;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            targetColor = originalColor;
        }
    }

}