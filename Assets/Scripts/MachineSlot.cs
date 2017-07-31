using Assets.Scripts.UI;
using UnityEngine;

namespace Assets.Scripts
{
    public class MachineSlot : MonoBehaviour
    {
        public bool HasMachine {  get { return Machine != null; } }
        public Vector3 PlacementOffset;
    
        public Machine Machine;
        private Light _light;

        public enum RoomTypes
        {
            InsideRoom,
            OutsideRoom,
            IceAsteroid,
            GasCloud,
            MineralAsteroid
        }

        public RoomTypes RoomType;

        void Start()
        {
            _light = GetComponentInChildren<Light>();
            if (Machine == null)
            {
                _light.intensity = 0f;
            }
        }

        void Update()
        {
        }

        public void PlaceMachine(MachineData machine)
        {
            if (HasMachine) return;
            if (machine.Prefab == null)
            {
                Debug.LogWarningFormat("Prefab is not set for machine {0}", machine.name);
                return;
            }
            Machine = Instantiate(machine.Prefab, this.transform).GetComponent<Machine>();
            Machine.transform.localPosition = PlacementOffset;
            GameManager.Instance.BuiltMachines.Add(machine);
            Machine.Place();
        }

        public void RemoveMachine()
        {
            GameManager.Instance.BuiltMachines.Remove(Machine.MachineData);
            Machine.Remove();
        }

        public void OnMouseClick()
        {
            UIBuildingMenu.Instance.Show(this);
        }
    }
}
