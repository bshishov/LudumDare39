using System;
using Assets.Scripts.UI;
using UnityEngine;

namespace Assets.Scripts
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class SpriteMachineAnimator : MonoBehaviour
    {
        private Machine _machine;
        private SpriteRenderer _renderer;

        private readonly Color _buildingColor = new Color(0.3f, 0.3f, 0.3f, 0.75f);

        void Start ()
        {
            _renderer = GetComponent<SpriteRenderer>();
            _machine = GetComponent<Machine>();
            if (_machine == null)
                _machine = GetComponentInParent<Machine>();
            _machine.StatusChanged += MachineOnStatusChanged;
            MachineOnStatusChanged(_machine.Status);
        }

        private void MachineOnStatusChanged(Machine.Statuses status)
        {
            if (status == Machine.Statuses.Crafting)
            {
                _renderer.material.color = Color.white;
                _renderer.material.SetFloat("_AnimationSpeed", 3);
            }
            else
                _renderer.material.SetFloat("_AnimationSpeed", 0);

            if (status == Machine.Statuses.Building)
                _renderer.material.color = _buildingColor;

            if (status == Machine.Statuses.Removing)
                _renderer.material.color = _buildingColor;

            if (status == Machine.Statuses.Idle)
                _renderer.material.color = Color.red;
        }

        void Update ()
        {
        }

        public void OnMouseEnter()
        {
            _renderer.material.SetColor("_Highlight", new Color(0.2f, 0.2f, 0.2f, 1f));
        }

        public void OnMouseLeave()
        {
            _renderer.material.SetColor("_Highlight", Color.black);
        }
    }
}
