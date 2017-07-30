using UnityEngine;

[CreateAssetMenu(fileName = "Resource", menuName = "Resource")]
public class ResourceData : ScriptableObject
{
    public string Name;
    public Sprite Icon;
    public float BaseAmount;

    public enum ResourceTypes
    {
        Cumulative,
        Density,
        Returnable
    }
    public ResourceTypes ResourceType = ResourceTypes.Cumulative;
}