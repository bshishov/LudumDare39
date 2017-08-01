using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts.UI
{
    [RequireComponent(typeof(UIResourcesListItem))]
    public class UIResourceEventTrigger : EventTrigger
    {
        public override void OnPointerEnter(PointerEventData data)
        {
            var item = GetComponent<UIResourcesListItem>();
            if (item != null && item.Resource != null)
            {
                UITooltip.Instance.Show(item.Resource);
                UIManager.Instance.PlayHoverSound();
            }
        }

        public override void OnPointerExit(PointerEventData data)
        {
            var item = GetComponent<UIResourcesListItem>();
            if (item != null && item.Resource != null)
                UITooltip.Instance.Hide();
        }

    }
}
