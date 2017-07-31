using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    [RequireComponent(typeof(Image))]
    [RequireComponent(typeof(Button))]
    public class UIMachineIcon : MonoBehaviour
    {
        public MachineData Machine {get { return _machine; }}
        private MachineData _machine;
        private Image _image;
        private Button _button;
        private MachineSlot _context;

        void Start ()
        {
            _image = GetComponent<Image>();
            _button = GetComponent<Button>();
            _button.onClick.AddListener(OnClick);
        }
	
        void Update ()
        {
            if(_machine != null && _context != null)
            {
                if (_machine.CanBeBuilt(_context))
                {
                    _button.interactable = true;
                }
                else
                {
                    _button.interactable = false;
                }
            }
            else
            {
                _button.interactable = false;
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
                _image = GetComponent<Image>();
                _image.sprite = data.Icon;
            }
        }

        public void SetRoom(MachineSlot context)
        {
            _context = context;
        }

        private void OnClick()
        {
            if (_context != null && _machine != null)
            {
                _context.PlaceMachine(_machine);
                UIBuildingMenu.Instance.Hide();
            }
        }
    }
}
