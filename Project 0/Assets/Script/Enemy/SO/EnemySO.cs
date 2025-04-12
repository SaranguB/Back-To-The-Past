using UnityEngine;

namespace Enemy
{
    [CreateAssetMenu(fileName = "Player", menuName = "ScriptableObjects/enemy")]
    public class EnemySO : ScriptableObject
    {
        public EnemyType enemyType;
        public int damage;
        public float speed;
        public bool InPast;
        public bool InPresent;
    }
}
