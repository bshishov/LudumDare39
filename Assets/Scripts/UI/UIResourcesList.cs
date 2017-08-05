using System.Collections.Generic;
using Assets.Scripts.Data;
using UnityEngine;

namespace Assets.Scripts.UI
{
    public class UIResourcesList : MonoBehaviour
    {
        public GameObject ResourcesListItemPrefab;
        public bool LoadAllAtStart = false;
        public bool AutoTracking = false;
        public bool CompareToStorage = false;

        private readonly Dictionary<ResourceData, UIResourcesListItem> _items = new Dictionary<ResourceData, UIResourcesListItem>();
	
        void Start()
        {
            if (ResourcesListItemPrefab == null)
                Debug.LogWarning("Prefab for resources list is not set", gameObject);

            if (LoadAllAtStart)
            {
                var resources = UnityEngine.Resources.LoadAll<ResourceData>("Res");
                foreach (var resource in resources)
                {
                    Add(resource);
                }
            }
        }

        public void Add(ResourceData resource, float amount = 0f)
        {
            if (_items.ContainsKey(resource))
            {
                var item = _items[resource];
                item.SetAmount(amount);
            }
            else
            {
                var itemObject = (GameObject)Instantiate(ResourcesListItemPrefab, transform);
                var item = itemObject.GetComponent<UIResourcesListItem>();
                item.Resource = resource;
                item.CompareToStorage = CompareToStorage;
                item.SetAmount(amount);

                if (AutoTracking)
                    item.ShowStorageAmount = true;

                _items.Add(resource, item);
            }
        }

        public void Add(ResourceAmount amount)
        {
            Add(amount.Resource, amount.Amount);
        }


        public void Remove(ResourceData resource)
        {
            if (_items.ContainsKey(resource))
            {
                var item = _items[resource];
                Destroy(item.gameObject);
                _items.Remove(resource);
            }
        }

        public void Clear()
        {
            foreach (var kvp in _items)
            {
                var item = kvp.Value;
                Destroy(item.gameObject);
            }
            _items.Clear();
        }
    }
}
