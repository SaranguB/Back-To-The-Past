using UnityEngine;

namespace Enemy
{
    [CreateAssetMenu(fileName = "Player", menuName = "ScriptableObjects/enemy")]
    public class EnemySO : ScriptableObject
    {
        public EnemyType enemyType;
        public int health;
        public int damage;
        public float speed;
        public bool inPast;
        public bool inPresent;
        public float attackRange;
        public float attackDelay;

        public float bombThrowForceX;
        public float bombThrowForceY;
        public float enemyWeponDelay;
        
    }
}
