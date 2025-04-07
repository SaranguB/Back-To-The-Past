using UnityEngine;

namespace Time
{
    [CreateAssetMenu(fileName = "new Object", menuName = "TimeAffectedObject")]
    public class TimeAffectedObjectsSO : ScriptableObject
    {
        public Sprite pastSprite;
        public Sprite presentSprite;

        public bool isMovableInPresent;
        public bool isMovableInPast;
    }
}
