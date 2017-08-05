using UnityEngine;

namespace Assets.Scripts.Data
{
    [CreateAssetMenu(fileName = "Resource", menuName = "Resource")]
    public class ResourceData : ScriptableObject
    {
        public string Name;
        public Sprite Icon;
        public string Description;

        public enum ResourceTypes
        {
            Cumulative,
            Density,
            Returnable
        }
        public ResourceTypes ResourceType = ResourceTypes.Cumulative;
    }
}