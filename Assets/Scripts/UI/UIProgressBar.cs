using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CanvasGroup))]
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

        _fill.anchorMax = new Vector2(Mathf.Lerp(0, _rectTransform.anchorMax.x, _oldValue), _fill.anchorMax.y);
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

    public void Hide()
    {
        GetComponent<CanvasGroup>().alpha = 0f;
    }

    public void Show()
    {
        GetComponent<CanvasGroup>().alpha = 1f;
    }
}
