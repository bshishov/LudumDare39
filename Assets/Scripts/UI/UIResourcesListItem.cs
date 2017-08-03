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
        public float StorageRedTreshold = 1000f;
        public bool ShowStorageAmount = false;
        public bool CompareToStorage = false;

        private float _amount = 0f;

        void Start ()
        {
            if (Resource != null)
            {
                Icon.sprite = Resource.Icon;
                Icon.preserveAspect = true;
            }
        }

        void Update()
        {
            if(Resource == null)
                return;

            if (ShowStorageAmount)
            {
                SetAmount(GameManager.Instance.Resources[Resource]);
                if (_amount < StorageRedTreshold)
                    AmountLabel.color = Color.red;
                else
                    AmountLabel.color = Color.white;
            }

            if (CompareToStorage)
            {
                var storageAmount = GameManager.Instance.Resources[Resource];
                if(_amount > storageAmount)
                    AmountLabel.color = Color.red;
                else
                    AmountLabel.color = Color.white;
            }
        }

        public void SetAmount(float amount)
        {
            if (Resource != null && AmountLabel != null)
            {
                _amount = amount;
                AmountLabel.text = amount.ToString("F0");
            }
        }
    }
}
