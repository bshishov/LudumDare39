using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.UI;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
[RequireComponent(typeof(Button))]
public class UIMachineIcon : MonoBehaviour
{
    private MachineData _machine;
    private Image _image;
    private Button _button;
    private Room _context;

	void Start ()
    {
        _image = GetComponent<Image>();
        _button = GetComponent<Button>();
        _button.onClick.AddListener(OnClick);
    }
	
	void Update ()
    {
		if(_machine != null && _context != null)
        {
            if (_machine.CanBeBuilt(_context))
            {
                _button.interactable = true;
            }
            else
            {
                _button.interactable = false;
            }
        }
		else
		{
		    _button.interactable = false;
		}
    }

    public void SetMachineData(MachineData data)
    {
        _machine = data;
        if (data.Icon == null)
        {
            Debug.LogWarning(string.Format("No icon for {0}", _machine.name));
        }
        else
        {
            _image.sprite = data.Icon;
        }
    }

    public void SetRoom(Room context)
    {
        _context = context;
    }

    private void OnClick()
    {
        if (_context != null && _machine != null)
        {
            _context.PlaceMachine(_machine);
            UIBuildingMenu.Instance.Hide();
        }
    }
}
