using System.Collections.Generic;
using Assets.Scripts.Utils;
using UnityEngine;
using System.Linq;

public class GameManager : Singleton<GameManager>
{
    public Dictionary<ResourceData, float> Resources = new Dictionary<ResourceData, float>();

	void Start ()
    {
        var resources = UnityEngine.Resources.LoadAll<ResourceData>("Resources"); 
        foreach(var resource in resources)
        {
            Resources.Add(resource, 0);
        }
    }	
	
	void Update ()
    {		
	}

    public void IncreaseResource(ResourceData resource, float amount)
    {
        Resources[resource] = amount + Resources[resource];
    }
}
