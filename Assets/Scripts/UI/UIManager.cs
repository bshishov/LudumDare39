﻿using Assets.Scripts.Utils;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts.UI
{
    public class UIManager : Singleton<UIManager>
    {
        public GameObject HoveredObject { get { return _hoveredObject; } }
        public GameObject ProgressBar;

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
                UnHover();
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
                
                go.SendMessage("OnMouseOver", SendMessageOptions.DontRequireReceiver);

                if (Input.GetMouseButtonDown(0))
                    go.SendMessage("OnMouseClick", 0, SendMessageOptions.DontRequireReceiver);

                if (Input.GetMouseButtonDown(1))
                    go.SendMessage("OnRightMouseClick", 0, SendMessageOptions.DontRequireReceiver);
            }
            else
            {
                UnHover();
                if (Input.GetMouseButtonDown(0))
                {
                    UIBuildingMenu.Instance.Hide();
                }
            }
        }

        private void UnHover()
        {
            if (_hoveredObject != null)
                _hoveredObject.SendMessage("OnMouseLeave", SendMessageOptions.DontRequireReceiver);
            _hoveredObject = null;
        }

        public UIProgressBar CreateProgressBar(GameObject go)
        {
            var pbObject = (GameObject) Instantiate(ProgressBar, transform);
            var pb = pbObject.GetComponent<UIProgressBar>();
            var follow = pbObject.GetComponent<UIFollowSceneObject>();
            follow.Target = go;
            return pb;
        }
    }
}
