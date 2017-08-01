﻿using Assets.Scripts.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts
{
    public class Sun : Singleton<Sun>
    {
        private List<Material> _sunMaterials;

        private float _temperature = 1f;

        public void Start()
        {
            _sunMaterials = GetComponentsInChildren<MeshRenderer>().Select(t => t.material).ToList();
            _sunMaterials.Add(GetComponent<MeshRenderer>().material);
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
