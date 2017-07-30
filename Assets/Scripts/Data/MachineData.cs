using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(menuName = "Item", fileName = "Item")]
public class MachineData : ScriptableObject
{
    [Header("Visual")]
    public string Name;
    public Sprite Icon;
    public GameObject Prefab;

    [Header("Construction")]
    public List<MachineData> RequiredMachines;
    public List<ResourceAmount> RequiredToBuildResources;
    public int TimeToBuild;
    public Room.RoomTypes AllowedRoomType;

    [Header("Deconstruction")]
    public List<ResourceAmount> ReturnedResources;
    public int TimeToDestroy;

    [Header("Production")]
    public List<ResourceAmount> InResourcesRequired;
    public List<ResourceAmount> InResources;
    public List<ResourceAmount> OutResources;
    public float TimeToProduce;

    public bool CanBeBuilt(Room room)
    {
        if (room.RoomType != AllowedRoomType) return false;
        if (RequiredToBuildResources.Any(t => !GameManager.Instance.HasResourceAmount(t))) return false;
        if (!RequiredMachines.All(t => GameManager.Instance.BuiltMachines.Contains(t))) return false;
        return true;
    }
}
