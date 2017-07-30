using System.Collections.Generic;
using Assets.Scripts.Utils;
using UnityEngine;

namespace Assets.Scripts.UI
{
    [RequireComponent(typeof(UIFollowSceneObject))]
    [RequireComponent(typeof(CanvasGroup))]
    public class UIBuildingMenu : Singleton<UIBuildingMenu>
    {
        public GameObject MachineIconPrefab;
        public GameObject IconsList;

        private CanvasGroup _canvasGroup;
        private List<UIMachineIcon> _icons;
        private UIFollowSceneObject _follow;

        void Start()
        {
            _follow = GetComponent<UIFollowSceneObject>();
            _icons = new List<UIMachineIcon>();
            _canvasGroup = GetComponent<CanvasGroup>();
            var machines = Resources.LoadAll<MachineData>("Machines");
            Debug.Log(string.Format("Loaded {0} machines", machines.Length));

            var targetTransform = this.transform;
            if (IconsList != null)
                targetTransform = IconsList.transform;

            foreach (var machine in machines)
            {
                var iconObject = (GameObject)Instantiate(MachineIconPrefab, targetTransform);
                var icon = iconObject.GetComponent<UIMachineIcon>();
                if (icon)
                {
                    icon.SetMachineData(machine);
                    _icons.Add(icon);
                }
            }

            Hide();
        }

        public void Hide()
        {
            _canvasGroup.alpha = 0f;
            _canvasGroup.interactable = false;
            _canvasGroup.blocksRaycasts = false;
        }

        public void Show(Room context)
        {
            _follow.Target = context.gameObject; 

            foreach (var icon in _icons)
            {
                icon.SetRoom(context);
            }

            _canvasGroup.alpha = 1f;
            _canvasGroup.interactable = true;
            _canvasGroup.blocksRaycasts = true;
        }
    }
}
