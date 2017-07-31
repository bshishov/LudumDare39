using Assets.Scripts.Data;
using Assets.Scripts.Utils;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    public class UIResourceTooltip : Singleton<UITooltip>
    {
        public Text NameLabel;
        public Image IconImage;
        public Text AmountLabel;
        public Text DescriptionLabel;

        private ResourceData _resource;

        void Start ()
        {
        }
        
        void Update ()
        {
            if(_resource != null)
            {
                if(AmountLabel != null)
                {
                    var amount = GameManager.Instance.Resources[_resource];
                    AmountLabel.text = string.Format("{0:F1}", amount);
                }              
            }
        }

        public void SetResource(ResourceData resource)
        {
            _resource = resource;
            if(_resource == null)
                return;

            if(NameLabel != null) 
                NameLabel.text = _resource.Name;

            if(IconImage != null && _resource.Icon != null)
                IconImage.sprite = _resource.Icon;

            if(DescriptionLabel != null && _resource.Description != null)
                DescriptionLabel.text = _resource.Description;
        }
    }
}
