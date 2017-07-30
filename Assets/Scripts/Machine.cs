using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Machine : MonoBehaviour
{
    public MachineData MachineData;

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
                _statusTimer += this.MachineData.TimeToBuild / Time.deltaTime;
                break;
            case Statuses.Removing:
                _statusTimer += this.MachineData.TimeToDestroy / Time.deltaTime;
                break;
            case Statuses.Crafting:
                _statusTimer += this.MachineData.TimeToProduce / Time.deltaTime;
                break;
            case Statuses.Idle:
                break;
        }
    }
}