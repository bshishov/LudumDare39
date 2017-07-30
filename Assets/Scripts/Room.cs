using UnityEngine;

public class Room : MonoBehaviour
{
    public bool HasEnhancement {  get { return Enhancement != null; } }
    
    public Enhancement Enhancement;

    void Start()
    {
    }

    void Update()
    {
    }

    public void PlaceEnhancement(Enhancement enhancement)
    {
        if (HasEnhancement) return;
        Enhancement = enhancement;        
    }

    public void RemoveEnchancement()
    {
        Enhancement = null;
    }
}
