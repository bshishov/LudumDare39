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
                _statusTimer += MachineData.TimeToBuild / Time.deltaTime;
                if (_statusTimer >= MachineData.TimeToBuild)
                {
                    _statusTimer = 0;
                    Status = Statuses.Crafting;
                }
                break;
            case Statuses.Removing:
                _statusTimer += MachineData.TimeToDestroy / Time.deltaTime;
                if (_statusTimer >= MachineData.TimeToDestroy)
                {
                    _statusTimer = 0;
                    GameObject.Destroy(gameObject);
                }
                break;
            case Statuses.Crafting:
                _statusTimer += MachineData.TimeToProduce / Time.deltaTime;
                if (_statusTimer >= MachineData.TimeToProduce)
                {
                    _statusTimer = 0;
                    foreach (var resourceAmount in MachineData.OutResources)
                    {
                        GameManager.Instance.IncreaseResource(resourceAmount);
                    }
                    if (MachineData.InResourcesRequired.Any(t => !GameManager.Instance.HasResourceAmount(t))) {
                        Status = Statuses.Idle;
                        break;
                    }
                    foreach (var resourceAmount in MachineData.InResources)
                    {
                        GameManager.Instance.DecreaseResource(resourceAmount);
                    }
                }
                else
                {
                    _statusTimer += MachineData.TimeToProduce / Time.deltaTime;
                }
                break;
            case Statuses.Idle:
                if (MachineData.InResourcesRequired.All(t => GameManager.Instance.HasResourceAmount(t)))
                {
                    Status = Statuses.Crafting;
                    break;
                }
                break;
        }
    }
}