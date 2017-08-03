using Assets.Scripts.Data;
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
        public Text DescriptionLabel;
        //public Text _description;

        public UIMachinesList RequiredMachines;
        public UIResourcesList RequiredResources;
        public UIResourcesList InResources;
        public UIResourcesList OutResources;

        void Start ()
        {
            if (InResources != null)
                InResources.CompareToStorage = true;

            if (RequiredResources != null)
                RequiredResources.CompareToStorage = true;
        }
        
        void Update ()
        {
            foreach (var res in _machineData.OutResources)
            {
                if (_machine != null)
                {
                    OutResources.Add(res.Resource, res.Amount * _machine.SunMultiplier);
                }
            }
        }

        public void SetMachineData(MachineData machineData)
        {
            _machineData = machineData;
            if (_machineData == null)
                return;

            if (_machine != null && _machine.MachineData != _machineData)
                _machine = null;

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

            if (DescriptionLabel != null)
            {
                if (string.IsNullOrEmpty(machineData.Description))
                {
                    DescriptionLabel.gameObject.SetActive(false);
                }
                else
                {
                    DescriptionLabel.text = machineData.Description;
                    DescriptionLabel.gameObject.SetActive(true);
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

            if (RequiredMachines != null)
            {
                RequiredMachines.Clear();
                foreach (var machine in _machineData.RequiredMachines)
                {
                    RequiredMachines.Add(machine);
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
                        text = "<color=red>Not enough resources</color>";
                        break;
                    case Machine.Statuses.Building:
                        text = "Construction...";
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
