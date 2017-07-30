using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIFollowSceneObject : MonoBehaviour
{
    public GameObject Target;
    public Vector3 Offset;

    private Camera _main;

	void Start ()
    {
        _main = Camera.main;
        var rectTransform = GetComponent<RectTransform>();

        /*
        if (rectTransform != null)
        {
            rectTransform.anchorMin = Vector2.zero;
            rectTransform.anchorMax = Vector2.one;
        }*/
	}	
	
	void Update ()
    {	
        if(Target != null)
        {
            var scr = _main.WorldToScreenPoint(Target.transform.position + Offset);
            transform.position = new Vector3(scr.x, scr.y, transform.position.z);
        }
	}
}
