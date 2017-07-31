using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts.UI
{
    [RequireComponent(typeof(UIMachineIcon))]
    class UIMachineEventTrigger : EventTrigger
    {
        public override void OnPointerEnter(PointerEventData data)
        {
            var item = GetComponent<UIMachineIcon>();
            if (item != null && item.Machine != null)
                UITooltip.Instance.Show(item.Machine);
        }

        public override void OnPointerExit(PointerEventData data)
        {
            var item = GetComponent<UIMachineIcon>();
            if (item != null && item.Machine != null)
                UITooltip.Instance.Hide();
        }
    }
}
