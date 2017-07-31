using Assets.Scripts.Utils;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    public class UIResourceTooltip : Singleton<UITooltip>
    {   
        private Resource _resource;
        private Text _nameLabel;
        private Image _icon;
        private Text _amount;
        private Text _description;

        void Start ()
        {
            // TODO: FIND LABELS
        }
        
        void Update ()
        {
            if(_resource != null)
            {
                if(_amount != null)
                {
                    var amount = GameManager.Instance.Resources[_resource];
                    _amount.text = string.Format("{0:F1}", amount);
                }                
            }
        }

        public void SetResource(Resource resource)
        {
            _resource = resource;
            if(_resource == null)
                return;

            if(_nameLabel != null) 
                _nameLabel.text = _resource.Name;

            if(_icon != null && _resource.Icon != null)
                _icon.sprite = _resource.Icon;

            if(_description != null && _resource.Desciption != null)
                _description.text = _resource.Description;
        }
    }
}
