using System.Collections.Generic;
using Assets.Scripts.Data;
using Assets.Scripts.Utils;
using UnityEngine;

namespace Assets.Scripts
{
    public class GameManager : Singleton<GameManager>
    {
        public Dictionary<ResourceData, float> Resources = new Dictionary<ResourceData, float>();

        public List<MachineData> BuiltMachines = new List<MachineData>();

        public GameObject Sun;
        public float TimeForSunToGoOut;

        private Sun _sunComponent;

        void Start()
        {
            var resources = UnityEngine.Resources.LoadAll<ResourceData>("Res"); 
            Debug.Log(string.Format("Loaded {0} resources", resources.Length));
            foreach(var resource in resources)
            {
                Resources.Add(resource, resource.BaseAmount);
            }
            _sunComponent = Sun.GetComponent<Sun>();
        }	
	
        void Update ()
        {
            if (_sunComponent.Temperature > 0f)
            {
                var oldTemperature = _sunComponent.Temperature;
                var newTemperature = oldTemperature - Time.deltaTime / TimeForSunToGoOut;
                _sunComponent.Temperature = Mathf.Max(newTemperature, 0f);
            }
        }

        public bool HasResourceAmount(ResourceAmount resource)
        {
            return Resources[resource.Resource] > resource.Amount;
        }

        public void DecreaseResource(ResourceAmount resource)
        {
            Resources[resource.Resource] = Resources[resource.Resource] - resource.Amount;
        }

        public void IncreaseResource(ResourceAmount resource)
        {
            Resources[resource.Resource] = resource.Amount + Resources[resource.Resource];
        }
    }
}
