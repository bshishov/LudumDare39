using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    [RequireComponent(typeof(Button))]
    public class UIRemover : MonoBehaviour
    {
        private Button _button;
        private bool _isRemoving;
        private Vector3 _initialPosition;

        void Start()
        {
            _button = GetComponent<Button>();
            _button.onClick.AddListener(OnClick);
            _isRemoving = false;
            _initialPosition = transform.position;
        }
	
        void Update()
        {
            if (_isRemoving)
            {
                transform.position = Input.mousePosition;
            }
        }

        void OnClick()
        {
            _isRemoving = true;
            _button.interactable = false;
        }

        public void Reset()
        {
            _isRemoving = false;
            transform.position = _initialPosition;
            _button.interactable = true;
        }
    }
}
