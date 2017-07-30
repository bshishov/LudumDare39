using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIResourcesListItem : MonoBehaviour
{
    public ResourceData Resource;
    private Text _amountLabel;
    private Image _icon;

	void Start ()
    {
        _amountLabel = GetComponentInChildren<Text>();
        _icon = GetComponentInChildren<Image>();
        _icon.sprite = Resource.Icon;
    }
	
    void Update()
    {
        var amount = GameManager.Instance.Resources[Resource];
        _amountLabel.text = string.Format("{0:F1}", amount);
    }	
}
