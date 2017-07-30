using Assets.Scripts.UI;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Machine : MonoBehaviour
{
    public MachineData MachineData;
    private UIProgressBar _progressBar;

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
            _status = value;
        }
    }

    private float _statusTimer;

    public void Init()
    {
        _progressBar = UIManager.Instance.CreateProgressBar(gameObject);
    }

    void Update()
    {
        switch (_status)
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
        _status = Statuses.Building;
        _progressBar.Show();
        ConsumeResources(MachineData.RequiredToBuildResources);
    }

    public void Remove()
    {
        _status = Statuses.Removing;
        _progressBar.Show();
    }
}