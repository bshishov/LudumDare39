using System;

[Serializable]
public class ResourceAmount
{
    public ResourceData Resource;
    public int Amount;

    public override string ToString()
    {
        return Resource.Name + " - " + Amount.ToString();
    }
}
