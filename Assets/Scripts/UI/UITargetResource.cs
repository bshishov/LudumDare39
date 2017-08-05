using UnityEngine;

namespace Assets.Scripts.UI
{
    [RequireComponent(typeof(UIResourcesListItem))]
    public class UITargetResource : MonoBehaviour
    {
        private UIResourcesListItem _item;

        // Use this for initialization
        void Start ()
        {
            _item = GetComponent<UIResourcesListItem>();
            if (GameManager.Instance.WinningCondition != null)
            {
                var w = GameManager.Instance.WinningCondition;
                _item.Icon.sprite = w.Resource.Icon;
                _item.Resource = w.Resource;
                _item.SetAmount(w.Amount);
            }
        }
    }
}
