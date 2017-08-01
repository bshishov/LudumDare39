using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Utils;
using UnityEngine;

namespace Assets.Scripts.UI
{
    [RequireComponent(typeof(UIFollowSceneObject))]
    [RequireComponent(typeof(CanvasGroup))]
    public class UIBuildingMenu : Singleton<UIBuildingMenu>
    {
        public UIMachinesList MachinesList;
        public bool IsActive { get { return _isActive; } }
        

        private MachineData[] _machines;
        private CanvasGroup _canvasGroup;
        private UIFollowSceneObject _follow;
        private bool _isActive;

        void Start()
        {
            _follow = GetComponent<UIFollowSceneObject>();
            _canvasGroup = GetComponent<CanvasGroup>();
            _machines = Resources.LoadAll<MachineData>("Machines");
            Debug.Log(string.Format("Loaded {0} machines", _machines.Length));

            if (MachinesList != null)
            {
                foreach (var machine in _machines)
                {
                    MachinesList.Add(machine);
                }
            }

            Hide();
        }

        public void Hide()
        {
            _canvasGroup.alpha = 0f;
            _canvasGroup.interactable = false;
            _canvasGroup.blocksRaycasts = false;
            _isActive = false;
        }

        public void Show(MachineSlot slot)
        {
            MachinesList.Clear();
            _isActive = true;
            _follow.SetTarget(slot.gameObject);

            if (MachinesList != null)
            {
                foreach (var machine in _machines)
                {
                    if (machine.AllowedRoomType == slot.RoomType)
                    {
                        MachinesList.Add(machine, slot);
                    }
                }
            }

            _canvasGroup.alpha = 1f;
            _canvasGroup.interactable = true;
            _canvasGroup.blocksRaycasts = true;
        }
    }
}
