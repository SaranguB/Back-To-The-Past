using UnityEngine;

namespace Wepons.Bomb
{
    [CreateAssetMenu(fileName = "Bomb", menuName = "ScriptableObjects/bombSO")]
    public class BombSO : ScriptableObject
    {
        public float Damage; 
    }
}
