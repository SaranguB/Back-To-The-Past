using UnityEngine;

namespace Audio
{
    [CreateAssetMenu(fileName = "SoundSO", menuName = "ScriptableObjects/SoundSO")]
    public class SoundSO : ScriptableObject
    {
       public Sounds[] audioList;
    }
}
