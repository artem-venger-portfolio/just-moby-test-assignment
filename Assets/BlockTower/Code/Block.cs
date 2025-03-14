using UnityEngine;
using UnityEngine.UI;

namespace BlockTower
{
    public class Block : MonoBehaviour
    {
        [SerializeField]
        private Image _image;

        public void SetDraggingColor()
        {
            _image.color = Color.green;
        }

        public void ResetColor()
        {
            _image.color = Color.white;
        }

        private void LogInfo(string message)
        {
            Debug.Log($"[{name}] {message}");
        }
    }
}