using Assets.Scripts.Data;
using Assets.Scripts.Utils;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    [RequireComponent(typeof(CanvasGroup))]
    public class UITooltip : Singleton<UITooltip>
    {        
        public UIResourceTooltip ResourceTooltip;
        public UIMachineTooltip MachineTooltip;

        private CanvasGroup _canvasGroup;
        private GameObject _currentTootlip;

        void Start ()
        {
            _canvasGroup = GetComponent<CanvasGroup>();
            
            ResourceTooltip = GetComponentInChildren<UIResourceTooltip>();
            MachineTooltip = GetComponentInChildren<UIMachineTooltip>();

            Hide();
        }
        
        void Update ()
        {
            var mouse = Input.mousePosition;

            if (_currentTootlip != null)
            {
                var h = ((RectTransform) _currentTootlip.transform).rect.height;
                if (mouse.y < h)
                {
                    transform.position = new Vector3(mouse.x, h, 0);
                }
                else
                {
                    transform.position = mouse;
                }
            }
        }

        public void ShowContainer()
        {
            _canvasGroup.alpha = 1f;
            _canvasGroup.blocksRaycasts = false;
            _canvasGroup.interactable = false;
        }

        public void Hide()
        {
            if(MachineTooltip != null)
                MachineTooltip.gameObject.SetActive(false);

            if (ResourceTooltip != null)
                ResourceTooltip.gameObject.SetActive(false);

            _canvasGroup.alpha = 0f;
            _canvasGroup.blocksRaycasts = false;
            _canvasGroup.interactable = false;
        }

        private void HideCurrentTooltip()
        {
            if(_currentTootlip != null)
                _currentTootlip.SetActive(false);
        }

        public void Show(Machine machine)
        {
            if(MachineTooltip != null)
            {
                HideCurrentTooltip();
                ShowContainer();
                MachineTooltip.gameObject.SetActive(true);
                MachineTooltip.SetMachine(machine);
                _currentTootlip = MachineTooltip.gameObject;
            }
        }

        public void Show(MachineData machine)
        {
            if (MachineTooltip != null)
            {
                HideCurrentTooltip();
                ShowContainer();
                MachineTooltip.gameObject.SetActive(true);
                MachineTooltip.SetMachineData(machine);
                _currentTootlip = MachineTooltip.gameObject;
            }
        }

        public void Show(ResourceData resource)
        {
            if(ResourceTooltip != null)
            {
                HideCurrentTooltip();
                ShowContainer();
                ResourceTooltip.gameObject.SetActive(true);
                ResourceTooltip.SetResource(resource);
                _currentTootlip = ResourceTooltip.gameObject;
            }
        }
    }
}
