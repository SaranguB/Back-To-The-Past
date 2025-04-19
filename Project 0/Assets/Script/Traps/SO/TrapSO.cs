using UnityEngine;

namespace Trap
{
    [CreateAssetMenu(fileName = "Trap", menuName = "ScriptableObjects/trapSo")]
    public class TrapSO : ScriptableObject
    {
        public TrapType trapType;
        public int damage;
        public float trapDelay;
    }
}
