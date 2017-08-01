using Assets.Scripts.Utils;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts.UI
{
    [RequireComponent(typeof(AudioSource))]
    public class UIManager : Singleton<UIManager>
    {
        public GameObject HoveredObject { get { return _hoveredObject; } }
        public GameObject ProgressBar;

        public AudioClipWithVolume HoverSound;
        public AudioClipWithVolume ClickSound;

        private Camera _camera;
        private AudioSource _audioSource;
        private GameObject _hoveredObject;
        private float _microDelay;

        const float MicroDelay = 0.1f;
        
        void Start()
        {
            _camera = Camera.main;
            _audioSource = GetComponent<AudioSource>();
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
                {
                    go.SendMessage("OnMouseClick", 0, SendMessageOptions.DontRequireReceiver);
                }

                if (Input.GetMouseButtonDown(1))
                {
                    go.SendMessage("OnRightMouseClick", 0, SendMessageOptions.DontRequireReceiver);
                }
            }
            else
            {
                UnHover();
                if (Input.GetMouseButtonDown(0))
                {
                    UIBuildingMenu.Instance.Hide();
                    UIBuildIcon.Instance.Hide();
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

        public void PlayClickSound(float mod = 1f, float pitch = 1f)
        {
            if(Time.time - _microDelay < MicroDelay)
                return;
            _microDelay = Time.time;
            _audioSource.pitch = pitch;
            if (ClickSound != null && ClickSound.Clip != null)
                _audioSource.PlayOneShot(ClickSound.Clip, ClickSound.VolumeModifier * mod);
        }

        public void PlayHoverSound(float mod = 1f, float pitch = 1f)
        {
            if (Time.time - _microDelay < MicroDelay)
                return;
            _microDelay = Time.time;
            _audioSource.pitch = pitch;
            if (HoverSound != null && HoverSound.Clip != null)
                _audioSource.PlayOneShot(HoverSound.Clip, HoverSound.VolumeModifier * mod);
        }
    }
}
