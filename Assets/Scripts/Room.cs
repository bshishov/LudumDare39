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

    public void PlaceMachine(Machine machine)
    {
        if (HasMachine) return;
        Machine = machine;        
    }

    public void RemoveMachine()
    {
        Machine = null;
    }
}
