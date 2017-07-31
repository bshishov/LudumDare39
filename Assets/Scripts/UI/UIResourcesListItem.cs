using System;
using Assets.Scripts.Data;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    public class UIResourcesListItem : MonoBehaviour
    {
        public ResourceData Resource;
        public Text AmountLabel;
        public Image Icon;

        void Start ()
        {
            if(Resource != null)
                Icon.sprite = Resource.Icon;
        }

        void OnMouseEnter(BaseEventData eventData)
        {
            UITooltip.Instance.Show(Resource);
        }

        void OnMouseLeave(BaseEventData eventData)
        {
            UITooltip.Instance.Hide();
        }

        void Update()
        {
            if (Resource != null && AmountLabel != null)
            {
                var amount = GameManager.Instance.Resources[Resource];
                AmountLabel.text = string.Format("{0:F1}", amount);
            }
        }	
    }
}
