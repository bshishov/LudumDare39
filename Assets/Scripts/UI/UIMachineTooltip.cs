using Assets.Scripts.Utils;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    public class UIMachineTooltip : Singleton<UITooltip>
    {   
        private Machine _machine;
        private Text _nameLabel;
        private Image _icon;
        private Text _amount;
        private Text _description;

        void Start ()
        {
            // TODO: FIND LABELS
        }
        
        void Update ()
        {
        }

        public void SetMachine(Machine machine)
        {
            _machine = machine;
            if(_machine == null)
                return;
        }
    }
}
