﻿using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Data
{
    [CreateAssetMenu(menuName = "Item", fileName = "Item")]
    public class MachineData : ScriptableObject
    {
        [Header("Visual")]
        public string Name;
        public Sprite Icon;
        public GameObject Prefab;
        public string Description;

        [Header("Construction")]
        public List<MachineData> RequiredMachines;
        public List<ResourceAmount> RequiredToBuildResources;
        public int TimeToBuild;
        public MachineSlot.RoomTypes AllowedRoomType;

        [Header("Deconstruction")]
        public List<ResourceAmount> ReturnedResources;
        public int TimeToDestroy;

        [Header("Production")]
        public List<ResourceAmount> InResourcesRequired;
        public List<ResourceAmount> InResources;
        public List<ResourceAmount> OutResources;
        public float TimeToProduce;
        public bool SunPowerDependent = false;

        public bool CanBeBuilt(MachineSlot machineSlot)
        {
            if (machineSlot.RoomType != AllowedRoomType)
                return false;

            if (RequiredToBuildResources.Any(t => !GameManager.Instance.HasResourceAmount(t)))
                return false;

            if (!RequiredMachines.All(md => GameManager.Instance.BuiltMachines.Any(m => m.MachineData.Equals(md))))
                return false;
            return true;
        }
    }
}
