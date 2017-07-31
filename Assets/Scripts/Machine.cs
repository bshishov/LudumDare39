using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.UI;
using UnityEngine;

namespace Assets.Scripts
{
    public class Machine : MonoBehaviour
    {
        public MachineData MachineData;
        private UIProgressBar _progressBar;
        private SpriteRenderer _renderer;

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
                if (value != _status)
                    OnStatusChange(_status, value);
                _status = value;
            }
        }

        private float _statusTimer;

        public void Start()
        {
            _renderer = GetComponent<SpriteRenderer>();
            _progressBar = UIManager.Instance.CreateProgressBar(gameObject);
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
                    _progressBar.Value = _statusTimer / (MachineData.TimeToBuild);
                    break;
                case Statuses.Removing:
                    if (TickTimer(MachineData.TimeToDestroy))
                    {
                        _statusTimer = 0;
                        GameObject.Destroy(gameObject);
                        GainResources(MachineData.ReturnedResources);
                        _progressBar.Hide();
                    }
                    _progressBar.Value = _statusTimer / (MachineData.TimeToBuild);
                    break;
                case Statuses.Crafting:
                    if (TickTimer(MachineData.TimeToProduce))
                    {
                        _statusTimer = 0;
                        GainResources(MachineData.OutResources);

                        if (HasEnoughResourcesToProduce())
                        {
                            ConsumeResources(MachineData.InResources);
                        }
                        else
                        {
                            Status = Statuses.Idle;
                        }
                    }
                    else
                    {
                        _statusTimer += MachineData.TimeToProduce / Time.deltaTime;
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

        public void GainResources(IEnumerable<ResourceAmount> resources)
        {
            foreach (var resourceAmount in resources)
            {
                GameManager.Instance.IncreaseResource(resourceAmount);
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
            _renderer.material.color = new Color(0.2f, 0.2f, 0.2f, 1f);
            UITooltip.Instance.Show(this);
        }

        public void OnMouseLeave()
        {
            _renderer.material.color = Color.black;
            UITooltip.Instance.Hide();   
        }

        private void OnStatusChange(Statuses oldStatus, Statuses newStatus)
        {
            if (newStatus == Statuses.Building)
            {
                if (_renderer == null)
                    _renderer = GetComponent<SpriteRenderer>();
                _renderer.material.color = new Color(0, 0, 0, 0.5f);
            }
            else
            {
                _renderer.material.color = Color.black;
            }

            if (newStatus == Statuses.Crafting)
            {
                StartAnimation();
            }
            else
            {
                StopAnimation();
            }

            if (newStatus == Statuses.Idle)
            {
                _renderer.material.color = Color.red;
            }
            else
            {
                _renderer.material.color = Color.black;
            }
        }

        private void StartAnimation()
        {
            _renderer.material.SetFloat("_AnimationSpeed", 3);
        }

        private void StopAnimation()
        {
            _renderer.material.SetFloat("_AnimationSpeed", 0);
        }
    }
}