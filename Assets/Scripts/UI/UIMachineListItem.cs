using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    [RequireComponent(typeof(Image))]
    [RequireComponent(typeof(Button))]
    public class UIMachineListItem : MonoBehaviour
    {
        public MachineData Machine { get { return _machine; }}
        public Image Icon;
        public Text NameLabel;
        private MachineData _machine;
        private Button _button;
        private MachineSlot _slot;

        void Start ()
        {
            _button = GetComponent<Button>();
            _button.onClick.AddListener(OnClick);
        }
	
        void Update ()
        {
            if(_machine != null && _slot != null)
            {
                if (_machine.CanBeBuilt(_slot))
                {
                    _button.interactable = true;
                }
                else
                {
                    _button.interactable = false;
                }
            }
        }

        public void SetMachineData(MachineData data)
        {
            _machine = data;

            if (data.Icon == null)
            {
                Debug.LogWarning(string.Format("No icon for {0}", _machine.name));
            }
            else
            {
                if (Icon != null)
                    Icon.sprite = data.Icon;
            }

            if (NameLabel != null)
                NameLabel.text = Machine.Name;
        }

        public void SetSlot(MachineSlot slot)
        {
            _slot = slot;
        }

        private void OnClick()
        {
            if (_slot != null && _machine != null)
            {
                _slot.PlaceMachine(_machine);
                UIBuildingMenu.Instance.Hide();
            }
        }
    }
}
