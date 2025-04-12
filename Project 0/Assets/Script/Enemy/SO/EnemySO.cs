using UnityEngine;

namespace Enemy
{
    [CreateAssetMenu(fileName = "Player", menuName = "ScriptableObjects/player")]
    public class EnemySO : ScriptableObject
    {
        public EnemyType enemyType;
        public int damage;
        public float speed;

    }
}
