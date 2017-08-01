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
        public List<ResourceAmount> ConsumePerSecond = new List<ResourceAmount>();

        public GameObject Sun;
        public float TimeForSunToGoOut;

        private Sun _sunComponent;
        private float _timer = 0f;

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

            _timer += Time.deltaTime;
            if (_timer > 1f)
            {

                _timer = 0;
            }
        }

        private void Consume()
        {
            foreach (var res in ConsumePerSecond)
            {
                DecreaseResource(res);
            }
        }

        public bool HasResourceAmount(ResourceAmount resource)
        {
            return Resources[resource.Resource] > resource.Amount;
        }

        public void DecreaseResource(ResourceAmount resource, float multiplier = 1)
        {
            Resources[resource.Resource] = Mathf.Max(Resources[resource.Resource] - resource.Amount * multiplier, 0);
        }

        public void IncreaseResource(ResourceAmount resource, float multiplier = 1)
        {
            Resources[resource.Resource] = resource.Amount * multiplier + Resources[resource.Resource];
        }
    }
}
