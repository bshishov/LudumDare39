using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Item", fileName = "Item")]
public class EnhancementData : ScriptableObject
{
    [Header("Visual")]
    public string Name;
    public Sprite Icon;
    public GameObject Prefab;

    [Header("Construction")]
    public List<ResourceAmount> RequiredToBuildResources;
    public int TimeToBuild;
    public int TimeToDestroy;

    [Header("Production")]
    public List<ResourceAmount> InResources;
    public List<ResourceAmount> OutResources;
    public float TimeToProduce;
}
