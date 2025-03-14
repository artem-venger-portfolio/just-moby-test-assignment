using UnityEngine;
using UnityEngine.EventSystems;

namespace BlockTower
{
    public class Block : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
    {
        public void OnBeginDrag(PointerEventData eventData)
        {
            LogInfo(nameof(OnBeginDrag));
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            LogInfo(nameof(OnEndDrag));
        }

        public void OnDrag(PointerEventData eventData)
        {
        }

        private void LogInfo(string message)
        {
            Debug.Log($"[{nameof(name)}] {message}");
        }
    }
}
