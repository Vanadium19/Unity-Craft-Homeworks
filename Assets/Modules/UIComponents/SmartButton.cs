using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Modules.UI
{
    public sealed class SmartButton : MonoBehaviour,
        IPointerDownHandler,
        IPointerUpHandler,
        IPointerEnterHandler,
        IPointerExitHandler
    {
        public event Action OnClick;
        public event Action OnHold;
        public event Action<PointerEventData> OnHover;
        public event Action<PointerEventData> OnUnhover;
        
        [SerializeField]
        private float _holdDuration = 0.5f;
        
        private float _startTime;
        private bool _pressed;

        void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
        {
            _startTime = Time.time;
            _pressed = true;
        }

        void IPointerUpHandler.OnPointerUp(PointerEventData eventData)
        {
            if (!_pressed)
                return;
            
            if (Time.time - _startTime < _holdDuration) 
                this.OnClick?.Invoke();
            
            _pressed = false;
        }

        void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
        {
            this.OnHover?.Invoke(eventData);
        }

        void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
        {
            this.OnUnhover?.Invoke(eventData);
        }

        private void Update()
        {
            if (!_pressed)
                return;

            if (Time.time - _startTime >= _holdDuration)
            {
                this.OnHold?.Invoke();
                _pressed = false;
            }
        }
    }
}