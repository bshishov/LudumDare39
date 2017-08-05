using System;

namespace Assets.Scripts.Data
{
    [Serializable]
    public class ResourceAmount
    {
        public ResourceData Resource;
        public float Amount;

        public override string ToString()
        {
            return Resource.Name + " - " + Amount.ToString();
        }
    }
}
