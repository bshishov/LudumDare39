using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIProgressBar : MonoBehaviour
{
    [Range(0f,1f)]
    public float Value;

    public float Speed = 1f;

    RectTransform _fill;    
    private float _oldValue = 0f;
    private RectTransform _rectTransform;

    void Start ()
    {
        _fill = (RectTransform)transform.GetChild(0);
        _rectTransform = GetComponent<RectTransform>();        
    }	
	
	void Update ()
    {		
        // TODO: Remove this testing
        if(Input.GetButtonDown("Jump"))
            Value = Random.value;

        _oldValue = Mathf.Lerp(_oldValue, Value, Time.deltaTime * Speed);
        
        // SLOW
        _fill.anchorMax = new Vector2(Mathf.Lerp(0, _rectTransform.anchorMax.x, _oldValue), _fill.anchorMax.y);          

        // INSTANT (better replace with one width change
        //_fill.anchorMax = new Vector2(Mathf.Lerp(0, _rectTransform.anchorMax.x, Value), _fill.anchorMax.y);
    }    
}
