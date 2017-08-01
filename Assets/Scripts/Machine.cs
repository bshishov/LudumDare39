using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.UI;
using UnityEngine;

namespace Assets.Scripts
{
    public class Machine : MonoBehaviour
    {
        public MachineData MachineData;
        public event Action<Statuses> StatusChanged;
        private UIProgressBar _progressBar;
        private Sun _sun;
        private float _statusTimer;

        public AudioClip BuildingSound;
        public AudioClip RemovingSound;

        public float SunMultiplier
        {
            get
            {
                var multiplier = 1f;
                if (MachineData.SunPowerDependent)
                {
                    multiplier = _sun.Temperature;
                }
                return multiplier;
            }
        }

        public enum Statuses
        {
            Idle,
            Building,
            Removing,
            Crafting
        }

        private Statuses _status;
        public Statuses Status
        {
            get { return _status; }
            set
            {
                if ((_status == Statuses.Building || _status == Statuses.Building) &&
                    _statusTimer != 0)
                {
                    return;
                }
                if (_status != value)
                {
                    switch (value)
                    {
                        case Statuses.Building:
                            GetComponent<AudioSource>().PlayOneShot(BuildingSound, 0.2f);
                            break;
                        case Statuses.Removing:
                            GetComponent<AudioSource>().PlayOneShot(RemovingSound, 0.3f);
                            break;
                    }
                }

                if (StatusChanged != null)
                    StatusChanged(value);
                _status = value;
            }
        }


        public void Start()
        {
            if(_progressBar == null)
                _progressBar = UIManager.Instance.CreateProgressBar(gameObject);
            _sun = GameManager.Instance.Sun.GetComponent<Sun>();

            StatusChanged += OnStatusChange;
        }

        void Update()
        {
            switch (Status)
            {
                case Statuses.Building:
                    if (TickTimer(MachineData.TimeToBuild))
                    {
                        _statusTimer = 0;
                        _progressBar.Hide();

                        if (HasEnoughResourcesToProduce())
                        {
                            Status = Statuses.Crafting;
                        }
                        else
                        {
                            Status = Statuses.Idle;
                        }
                    }
                    _progressBar.Value = _statusTimer / MachineData.TimeToBuild;
                    break;
                case Statuses.Removing:
                    if (TickTimer(MachineData.TimeToDestroy))
                    {
                        _statusTimer = 0;
                        GameObject.Destroy(gameObject);
                        GainResources(MachineData.ReturnedResources);
                        _progressBar.Hide();
                        Destroy(_progressBar.gameObject);
                    }
                    _progressBar.Value = _statusTimer / MachineData.TimeToBuild;
                    break;
                case Statuses.Crafting:
                    if (TickTimer(MachineData.TimeToProduce))
                    {
                        _statusTimer = 0;
                        GainResources(MachineData.OutResources, true);

                        if (HasEnoughResourcesToProduce())
                        {
                            ConsumeResources(MachineData.InResources);
                        }
                        else
                        {
                            Status = Statuses.Idle;
                        }
                    }
                    break;
                case Statuses.Idle:
                    if (HasEnoughResourcesToProduce())
                    {
                        Status = Statuses.Crafting;
                        ConsumeResources(MachineData.InResources);
                        TickTimer(MachineData.TimeToProduce);
                    }
                    break;
            }
        }

        private bool TickTimer(float time)
        {
            _statusTimer += Time.deltaTime;
            return _statusTimer >= time;
        }

        public bool HasEnoughResourcesToProduce()
        {
            return MachineData.InResourcesRequired.All(t => GameManager.Instance.HasResourceAmount(t)) &&
                   MachineData.InResources.All(t => GameManager.Instance.HasResourceAmount(t));
        }

        public void ConsumeResources(IEnumerable<ResourceAmount> resources)
        {
            foreach (var resourceAmount in resources)
            {
                GameManager.Instance.DecreaseResource(resourceAmount);
            }
        }

        public void GainResources(IEnumerable<ResourceAmount> resources, bool multiplied = false)
        {
            foreach (var resourceAmount in resources)
            {
                GameManager.Instance.IncreaseResource(resourceAmount, multiplied ? SunMultiplier : 1);
            }
        }

        public void Place()
        {
            Status = Statuses.Building;
            if (_progressBar == null)
                _progressBar = UIManager.Instance.CreateProgressBar(gameObject);
            _progressBar.Show();
            ConsumeResources(MachineData.RequiredToBuildResources);
        }

        public void Remove()
        {
            Status = Statuses.Removing;
            _progressBar.Show();
        }

        public void OnMouseEnter()
        {
            UITooltip.Instance.Show(this);
        }

        public void OnMouseLeave()
        {
            UITooltip.Instance.Hide();   
        }

        private void OnStatusChange(Statuses newStatus)
        {
            if (newStatus == Statuses.Crafting || newStatus == Statuses.Idle)
                _progressBar.Hide();
        }
    }
}