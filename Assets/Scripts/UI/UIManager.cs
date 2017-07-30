using Assets.Scripts.Utils;
using UnityEngine;

namespace Assets.Scripts.UI
{
    public class UIManager : Singleton<UIManager>
    {
        private Camera _camera;
        
        void Start ()
        {
            _camera = Camera.main;
        }
	
        void Update ()
        {
            var mouseRay = _camera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(mouseRay, out hit))
            {
                Debug.Log(hit);
                hit.collider.SendMessage("OnMouseOver", SendMessageOptions.DontRequireReceiver);

                if (Input.GetMouseButtonDown(0))
                    hit.collider.SendMessage("OnMouseClick", 0, SendMessageOptions.DontRequireReceiver);
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
