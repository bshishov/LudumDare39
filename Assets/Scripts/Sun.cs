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
        public List<Sprite> Smiles;

        private List<Material> _sunMaterials;
        private float _temperature = 1f;
        private SpriteRenderer _smileRenderer;
        

        public void Start()
        {
            _sunMaterials = GetComponentsInChildren<MeshRenderer>().Select(t => t.material).ToList();
            _sunMaterials.Add(GetComponent<MeshRenderer>().material);
            _smileRenderer = GetComponentInChildren<SpriteRenderer>();
        }

        public void Update()
        {
            var i = Mathf.RoundToInt(_temperature * (Smiles.Count - 1));
            _smileRenderer.sprite = Smiles[i];
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
