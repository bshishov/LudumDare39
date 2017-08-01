using Assets.Scripts.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts
{
    public class Sun : Singleton<MonoBehaviour>
    {
        public List<GameObject> SunParts;
        private List<Material> _sunMaterials;

        private float _temperature = 1f;

        public void Start()
        {
            _sunMaterials = SunParts.Select(t => t.GetComponent<MeshRenderer>().material).ToList();
        }

        public float Temperature
        {
            get { return _temperature; }
            set {
                _temperature = value;
                foreach (var arch in _sunMaterials)
                {
                    arch.SetFloat("_Temperature", _temperature);
                }
            }
        }
    }
}
