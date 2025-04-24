using System;
using UnityEngine;

namespace Audio
{
    [Serializable]
    public struct Sounds
    {
        public SoundType soundType;
        public AudioClip audioClip;
    }
}
