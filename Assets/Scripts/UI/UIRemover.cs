using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    [RequireComponent(typeof(Image))]
    [RequireComponent(typeof(Button))]
    public class UIRemover : MonoBehaviour
    {
        private Button _button;
        private Image _image;
        private bool _isRemoving;
        private Vector3 _initialPosition;

        void Start()
        {
            _button = GetComponent<Button>();
            _image = GetComponent<Image>();
            _button.onClick.AddListener(OnClick);
            _isRemoving = false;
            _initialPosition = transform.position;
        }
	
        void Update()
        {
            if (_isRemoving)
            {
                transform.position = Input.mousePosition + new Vector3(-0.1f, 0.1f);

                if (Input.GetMouseButtonDown(0))
                {
                    var obj = UIManager.Instance.HoveredObject;
                    if (obj != null)
                    {
                        var machine = obj.GetComponent<Machine>();
                        if(machine != null)
                            machine.Remove();
                    }
                    Reset();
                }
            }
        }

        void OnClick()
        {
            _isRemoving = true;
            _image.raycastTarget = false;
            _button.interactable = false;
        }

        public void Reset()
        {
            _isRemoving = false;
            _image.raycastTarget = true;
            transform.position = _initialPosition;
            _button.interactable = true;
        }
    }
}
