using Assets.Scripts.Utils;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    [RequireComponent(typeof(Image))]
    [RequireComponent(typeof(UIFollowSceneObject))]
    public class UIBuildIcon : Singleton<UIBuildIcon>
    {
        private UIFollowSceneObject _follow;
        private Image _image;
        
        void Start ()
        {
            _follow = GetComponent<UIFollowSceneObject>();
            _image = GetComponent<Image>();
        }

        public void Show(GameObject go)
        {
            _follow.SetTarget(go);
            _image.CrossFadeAlpha(1f, 0.2f, false);
        }

        public void Hide()
        {
            _follow.SetTarget(null);
            _image.CrossFadeAlpha(0f, 0.2f, false);
        }
    }
}
