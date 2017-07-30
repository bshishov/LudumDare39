using Assets.Scripts.Utils;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts.UI
{
    public class UIManager : Singleton<UIManager>
    {
        private Camera _camera;
        private GameObject _hoveredObject;
        
        void Start()
        {
            _camera = Camera.main;
        }
	
        void Update()
        {
            if (EventSystem.current.IsPointerOverGameObject())
            {
                Debug.Log("OVER SCREEN");
                return;
            }

            var mouseRay = _camera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(mouseRay, out hit, LayerMask.GetMask("Default", "UI")))
            {
                var go = hit.collider.gameObject;

                if (go != _hoveredObject)
                {
                    if (_hoveredObject != null)
                        _hoveredObject.SendMessage("OnMouseLeave", SendMessageOptions.DontRequireReceiver);

                    _hoveredObject = go;
                    go.SendMessage("OnMouseEnter", SendMessageOptions.DontRequireReceiver);
                }

                Debug.Log(hit);
                go.SendMessage("OnMouseOver", SendMessageOptions.DontRequireReceiver);

                if (Input.GetMouseButtonDown(0))
                    go.SendMessage("OnMouseClick", 0, SendMessageOptions.DontRequireReceiver);

                if (Input.GetMouseButtonDown(1))
                    go.SendMessage("OnRightMouseClick", 0, SendMessageOptions.DontRequireReceiver);
            }
            else
            {
                if (Input.GetMouseButtonDown(0))
                {
                    UIBuildingMenu.Instance.Hide();
                }
            }
        }
    }
}
