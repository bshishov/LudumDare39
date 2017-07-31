using Assets.Scripts.Data;
using UnityEngine;
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
            Icon.sprite = Resource.Icon;
        }
	
        void Update()
        {
            var amount = GameManager.Instance.Resources[Resource];
            AmountLabel.text = string.Format("{0:F1}", amount);
        }	
    }
}
