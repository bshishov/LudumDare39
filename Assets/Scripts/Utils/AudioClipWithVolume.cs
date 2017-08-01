using System;
using UnityEngine;

namespace Assets.Scripts.Utils
{
    [Serializable]
    public class AudioClipWithVolume
    {
        public AudioClip Clip;

        [Range(0f, 1f)]
        public float VolumeModifier = 1f;
    }
}
