using System.Collections.Generic;
using System.Linq;
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
        public Text ChangeLabel;
        public Text DescriptionLabel;
        public UIMachinesList CanBeProducedFrom;

        private ResourceData _resource;
        private MachineData[] _machines;

        void Awake ()
        {
            print("START");
            _machines = Resources.LoadAll<MachineData>("Machines");
        }
        
        void Update ()
        {
            if(_resource != null)
            {
                if(AmountLabel != null)
                {
                    if (GameManager.Instance.Resources.ContainsKey(_resource))
                    {
                        var amount = GameManager.Instance.Resources[_resource];
                        AmountLabel.text = string.Format("{0:F1}", amount);
                    }
                    else
                    {
                        AmountLabel.text = "N/A";
                    }
                }

                if (ChangeLabel != null)
                {
                    var change = 0f;

                    foreach (var machine in GameManager.Instance.BuiltMachines)
                    {
                        if(machine.Status != Machine.Statuses.Crafting)
                            break;

                        var resIn = machine.MachineData.InResources.FirstOrDefault(r => r.Resource.Equals(_resource));
                        if (resIn != null)
                            change -= resIn.Amount / machine.MachineData.TimeToProduce;

                        var resOut = machine.MachineData.OutResources.FirstOrDefault(r => r.Resource.Equals(_resource));
                        if (resOut != null)
                            change += resOut.Amount / machine.MachineData.TimeToProduce;
                    }

                    var resConsume = GameManager.Instance.ConsumePerSecond.FirstOrDefault(r => r.Resource == _resource);
                    if (resConsume != null)
                        change -= resConsume.Amount;

                    if (change < 0)
                    {
                        ChangeLabel.color = Color.red;
                        ChangeLabel.text = string.Format("{0:F1}/s", change);
                    }
                    else
                    {
                        ChangeLabel.color = Color.green;
                        ChangeLabel.text = string.Format("+{0:F1}/s", change);
                    }
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

            if (IconImage != null)
            {
                if (_resource.Icon != null)
                    IconImage.sprite = _resource.Icon;
                else
                    IconImage.sprite = null;
            }

            if (DescriptionLabel != null)
            {
                if (resource.Description != null)
                    DescriptionLabel.text = _resource.Description;
                else
                    DescriptionLabel.text = "";
            }

            if (CanBeProducedFrom != null)
            {
                CanBeProducedFrom.Clear();
                foreach (var machine in _machines)
                {
                    if(machine.OutResources.Any(r=>r.Resource.Equals(_resource)))
                        CanBeProducedFrom.Add(machine);
                }
            }
        }
    }
}
