using Assets.Scripts.UI;
using UnityEngine;

public class Room : MonoBehaviour
{
    public bool HasMachine {  get { return Machine != null; } }
    
    public Machine Machine;

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
    }

    void Update()
    {
    }

    public void PlaceMachine(MachineData machine)
    {
        if (HasMachine) return;
        Machine = Instantiate(machine.Prefab, this.transform).GetComponent<Machine>();
        GameManager.Instance.BuiltMachines.Add(machine);
    }

    public void RemoveMachine()
    {
        Machine = null;
    }

    public void OnMouseClick()
    {
        UIBuildingMenu.Instance.Show(this);
    }
}
