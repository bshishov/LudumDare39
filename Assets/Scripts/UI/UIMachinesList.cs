using System.Collections.Generic;
using Assets.Scripts.Data;
using UnityEngine;

namespace Assets.Scripts.UI
{
    public class UIMachinesList : MonoBehaviour
    {
        public GameObject MachineListItemPrefab;

        private readonly Dictionary<MachineData, UIMachineListItem> _items = new Dictionary<MachineData, UIMachineListItem>();

        
        void Start ()
        {
            if (MachineListItemPrefab == null)
                Debug.LogWarning("Prefab for machines list is not set", gameObject);
        }

        public void Add(MachineData machine, MachineSlot slot = null)
        {
            if (_items.ContainsKey(machine))
            {
                _items[machine].SetMachineData(machine);
                if (slot != null)
                    _items[machine].SetSlot(slot);
                return;
            }

            var itemObject = (GameObject)Instantiate(MachineListItemPrefab, transform);
            var item = itemObject.GetComponent<UIMachineListItem>();
            item.SetMachineData(machine);
            if(slot != null)
                item.SetSlot(slot);

            _items.Add(machine, item);
        }

        public void Remove(MachineData machine)
        {
            if (_items.ContainsKey(machine))
            {
                var item = _items[machine];
                Destroy(item.gameObject);
                _items.Remove(machine);
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
