using Assets.Scripts.Data;
using Assets.Scripts.Utils;
using UnityEngine;

namespace Assets.Scripts.UI
{
    [RequireComponent(typeof(CanvasGroup))]
    public class UITooltip : Singleton<UITooltip>
    {        
        public UIResourceTooltip ResourceTooltip;
        public UIMachineTooltip MachineTooltip;
        //public UIMachineDataTooltip MachineDataTooltip;

        private CanvasGroup _canvasGroup;
        private GameObject _currentTootlip;

        void Start ()
        {
            _canvasGroup = GetComponent<CanvasGroup>();
            
            ResourceTooltip = GetComponentInChildren<UIResourceTooltip>();
            MachineTooltip = GetComponentInChildren<UIMachineTooltip>();
            //MachineDataTooltip = GetComponentInChildren<UIMachineDataTooltip>();

            Hide();
        }
        
        void Update ()
        {
            transform.position = Input.mousePosition;
        }

        public void ShowContainer()
        {
            _canvasGroup.alpha = 1f;
            _canvasGroup.blocksRaycasts = false;
            _canvasGroup.interactable = false;
        }

        public void Hide()
        {
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
            }
        }

        public void Show(MachineData machine)
        {            
        }

        public void Show(ResourceData resource)
        {
            if(ResourceTooltip != null)
            {
                HideCurrentTooltip();
                ShowContainer();
                ResourceTooltip.gameObject.SetActive(true);
                ResourceTooltip.SetResource(resource);
            }
        }
    }
}
