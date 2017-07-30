using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Machine : MonoBehaviour
{
    public MachineData MachineData;
    public UIProgressBar ProgressBar;

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

    void Start()
    {

    }

    void Update()
    {
        switch (_status)
        {
            case Statuses.Building:
                if (TickTimer(MachineData.TimeToBuild))
                {
                    _statusTimer = 0;
                    Status = Statuses.Crafting;
                }
                break;
            case Statuses.Removing:
                if (TickTimer(MachineData.TimeToDestroy))
                {
                    _statusTimer = 0;
                    GameObject.Destroy(gameObject);
                    ReturnResources();
                }
                break;
            case Statuses.Crafting:
                if (TickTimer(MachineData.TimeToProduce))
                {
                    _statusTimer = 0;
                    ProduceResources();

                    if (HasEnoughResourcesToProduce())
                    {
                        ConsumeResources();
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
                    ConsumeResources();
                    TickTimer(MachineData.TimeToProduce);
                }
                break;
        }
    }

    private bool TickTimer(float time)
    {
        _statusTimer += time / Time.deltaTime;
        return _statusTimer >= time;
    }

    public bool HasEnoughResourcesToProduce()
    {
        return MachineData.InResourcesRequired.All(t => GameManager.Instance.HasResourceAmount(t)) &&
            MachineData.InResources.All(t => GameManager.Instance.HasResourceAmount(t));
    }

    public void ProduceResources()
    {
        foreach (var resourceAmount in MachineData.OutResources)
        {
            GameManager.Instance.IncreaseResource(resourceAmount);
        }
    }

    public void ConsumeResources()
    {
        foreach (var resourceAmount in MachineData.InResources)
        {
            GameManager.Instance.DecreaseResource(resourceAmount);
        }
    }

    public void ReturnResources()
    {
        foreach (var resourceAmount in MachineData.ReturnedResources)
        {
            GameManager.Instance.IncreaseResource(resourceAmount);
        }
        foreach (var resourceAmount in MachineData.ReturnedResources.Where(t => t.Resource.ResourceType == ResourceData.ResourceTypes.Returnable))
        {
            // todo: return resources
        }
    }
}