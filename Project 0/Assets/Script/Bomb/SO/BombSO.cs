using UnityEngine;

namespace Wepons.Bomb
{
    [CreateAssetMenu(fileName = "Bomb", menuName = "ScriptableObjects/bombSO")]
    public class BombSO : ScriptableObject
    {
        public float bombDamage;
        public float DamageRadius;
        public float primingTime;
    }
}
