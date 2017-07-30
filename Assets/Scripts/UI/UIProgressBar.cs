using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIProgressBar : MonoBehaviour
{
    [Range(0f,1f)]
    public float Value;

    public float Speed = 1f;

    RectTransform _fill;    
    private float _oldValue = 0f;
    private RectTransform _rectTransform;
    private Image _image;

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
        _fill.anchorMax = new Vector2(Mathf.Lerp(0, _rectTransform.anchorMax.x, _oldValue) * 2, _fill.anchorMax.y);          

        // INSTANT (better replace with one width change
        //_fill.anchorMax = new Vector2(Mathf.Lerp(0, _rectTransform.anchorMax.x, Value), _fill.anchorMax.y);
    }

    public void Hide()
    {
        _image = GetComponent<Image>();
        _image.CrossFadeAlpha(0f, 1f, false);
    }

    public void Show()
    {
        _image = GetComponent<Image>();
        _image.CrossFadeAlpha(1f, 1f, false);
    }
}
