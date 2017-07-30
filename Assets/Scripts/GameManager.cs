using System.Collections.Generic;
using Assets.Scripts.Utils;
using UnityEngine;
using System.Linq;

public class GameManager : Singleton<GameManager>
{
    public Dictionary<ResourceData, float> Resources = new Dictionary<ResourceData, float>();

    public List<MachineData> BuiltMachines = new List<MachineData>();

	void Start()
    {
        var resources = UnityEngine.Resources.LoadAll<ResourceData>("Res"); 
        Debug.Log(string.Format("Loaded {0} resources", resources.Length));
        foreach(var resource in resources)
        {
            Resources.Add(resource, 0);
        }
    }	
	
	void Update ()
    {		
	}

    public bool HasResourceAmount(ResourceAmount resource)
    {
        return Resources[resource.Resource] > resource.Amount;
    }

    public void DecreaseResource(ResourceAmount resource)
    {
        Resources[resource.Resource] = Resources[resource.Resource] - resource.Amount;
    }

    public void IncreaseResource(ResourceAmount resource)
    {
        Resources[resource.Resource] = resource.Amount + Resources[resource.Resource];
    }
}
