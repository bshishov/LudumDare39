using UnityEngine;

public class Room : MonoBehaviour
{
    public bool HasMachine {  get { return Machine != null; } }
    
    public Machine Machine;

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
