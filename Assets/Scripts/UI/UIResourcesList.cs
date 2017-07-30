using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIResourcesList : MonoBehaviour
{
    public GameObject ResourcesListItemPrefab;    
	
	void Start()
    {
        var resources = UnityEngine.Resources.LoadAll<ResourceData>("Res");
        foreach (var resource in resources)
        {
            var itemObject = (GameObject)Instantiate(ResourcesListItemPrefab, transform);
            var item = itemObject.GetComponent<UIResourcesListItem>();
            item.Resource = resource;            
        }
	}
}
