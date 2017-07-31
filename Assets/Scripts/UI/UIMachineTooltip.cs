using Assets.Scripts.Utils;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    public class UIMachineTooltip : Singleton<UITooltip>
    {   
        private Machine _machine;
        private MachineData _machineData;
        public Text NameLabel;
        public Image Icon;
        public Text StatusLabel;
        public Text RequiredTime;
        //public Text _description;

        public UIResourcesList RequiredResources;
        public UIResourcesList InResources;
        public UIResourcesList OutResources;

        void Start ()
        {
        }
        
        void Update ()
        {
        }

        public void SetMachineData(MachineData machineData)
        {
            _machineData = machineData;
            if (_machineData == null)
                return;

            if (NameLabel != null)
            {
                NameLabel.text = _machineData.Name;
            }

            if (StatusLabel != null)
            {
                StatusLabel.text = "Machine";
            }

            if (Icon != null)
            {
                if (machineData.Icon != null)
                {
                    Icon.sprite = machineData.Icon;
                }
                else
                {
                    Icon.sprite = null;
                }
            }

            if (RequiredTime != null)
            {
                RequiredTime.text = string.Format("{0}s", machineData.TimeToProduce);
            }

            if (InResources != null)
            {
                InResources.Clear();
                foreach (var res in _machineData.InResources)
                {
                    InResources.Add(res);
                }
            }

            if (OutResources != null)
            {
                OutResources.Clear();
                foreach (var res in _machineData.OutResources)
                {
                    OutResources.Add(res);
                }
            }

            if (RequiredResources != null)
            {
                RequiredResources.Clear();
                foreach (var res in _machineData.RequiredToBuildResources)
                {
                    RequiredResources.Add(res);
                }
            }
        }
        
        public void SetMachine(Machine machine)
        {
            _machine = machine;
            if(_machine == null)
                return;
            SetMachineData(_machine.MachineData);

            if (StatusLabel != null)
            {
                var text = "";
                switch (machine.Status)
                {
                    case Machine.Statuses.Idle:
                        text = "Idle";
                        break;
                    case Machine.Statuses.Building:
                        text = "Building";
                        break;
                    case Machine.Statuses.Crafting:
                        text = "Producing";
                        break;
                    case Machine.Statuses.Removing:
                        text = "Removing";
                        break;
                }
                StatusLabel.text = text;
            }
        }
    }
}
