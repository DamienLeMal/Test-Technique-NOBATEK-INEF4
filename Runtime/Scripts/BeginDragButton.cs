
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace DamienLeMal.TearOffMenu {
    public class BeginDragButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler {
        public UnityEvent PointerDown;
        public UnityEvent PointerUp;
        private Vector2 pointerStartPos;

        public void OnPointerDown(PointerEventData eventData) {
            PointerDown.Invoke();
            pointerStartPos = eventData.position;
        }

        public void OnPointerUp(PointerEventData eventData) {
            if (pointerStartPos == eventData.position)
                PointerUp.Invoke();
        }
    }
}