using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIResourcesList : MonoBehaviour
{
    public GameObject ResourcesListItemPrefab;    
	
	void Start ()
    {   
		foreach(var resource in GameManager.Instance.Resources.Keys)
        {
            var itemObject = (GameObject)Instantiate(ResourcesListItemPrefab, transform);
            var item = itemObject.GetComponent<UIResourcesListItem>();
            item.Resource = resource;            
        }
	}
}
