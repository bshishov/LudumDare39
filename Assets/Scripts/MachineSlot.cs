using Assets.Scripts.Data;
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
        private Renderer _renderer;
        private Color _withoutMachine = new Color(0.5f, 0.5f, 0.5f, 1f);
        private Color _withMachine = Color.white;

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
            _renderer = GetComponent<Renderer>();
            if (Machine == null)
            {
                // if(_light != null)
                //   _light.intensity = 0f;

                if (_renderer != null)
                    _renderer.material.color = _withoutMachine;
            }
        }

        void Update()
        {
        }

        public void PlaceMachine(MachineData machine)
        {
            if (HasMachine)
                return;
            if (machine.Prefab == null)
            {
                Debug.LogWarningFormat("Prefab is not set for machine {0}", machine.name);
                return;
            }
            Machine = Instantiate(machine.Prefab, this.transform).GetComponent<Machine>();
            Machine.transform.localPosition = PlacementOffset;
            GameManager.Instance.BuiltMachines.Add(Machine);
            Machine.Place();
            UIBuildIcon.Instance.Hide();

            if (_renderer != null)
                _renderer.material.color = _withMachine;
        }

        public void OnMachineRemoved()
        {
            GameManager.Instance.BuiltMachines.Remove(Machine);

            if (_renderer != null)
                _renderer.material.color = _withoutMachine;
        }

        public void CustomOnMouseClick()
        {
            if (!HasMachine)
            {
                UIBuildIcon.Instance.Show(gameObject);
                UIBuildingMenu.Instance.Show(this);
                UIManager.Instance.PlayClickSound();
            }
        }

        public void CustomOnMouseEnter()
        {
            if (!HasMachine)
            {
                if (!UIBuildingMenu.Instance.IsActive)
                    UIBuildIcon.Instance.Show(gameObject);
                UIManager.Instance.PlayHoverSound(pitch: 0.8f);
            }
        }

        public void CustomOnMouseLeave()
        {
            if (!HasMachine)
            {
                if (!UIBuildingMenu.Instance.IsActive)
                    UIBuildIcon.Instance.Hide();
            }
        }
    }
}
