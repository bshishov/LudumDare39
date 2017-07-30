using Assets.Scripts.Utils;
using UnityEngine;

namespace Assets.Scripts.UI
{
    [RequireComponent(typeof(CanvasGroup))]
    public class UITooltip : Singleton<UITooltip>
    {
        private CanvasGroup _canvasGroup;

        void Start ()
        {
            _canvasGroup = GetComponent<CanvasGroup>();
            Hide();
        }
        
        void Update ()
        {
            transform.position = Input.mousePosition;
        }

        void Show()
        {
            _canvasGroup.alpha = 1f;
            _canvasGroup.blocksRaycasts = true;
            _canvasGroup.interactable = true;
        }

        void Hide()
        {
            _canvasGroup.alpha = 0f;
            _canvasGroup.blocksRaycasts = false;
            _canvasGroup.interactable = false;
        }


    }
}
