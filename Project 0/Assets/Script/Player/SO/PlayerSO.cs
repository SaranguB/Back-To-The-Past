using UnityEngine;

namespace Player
{
    [CreateAssetMenu(fileName ="Player", menuName = "ScriptableObjects/player")]
    public class PlayerSO : ScriptableObject    
    {
        public float movementSpeed;
        public int health;
        public float jumpForce;
        public float fallingSpeed;
    }
}
