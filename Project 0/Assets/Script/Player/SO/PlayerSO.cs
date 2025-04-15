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
        public float dashSpeed;
        public float dashDuration;
        public int maxDashes;
        public int currentDashes;
        public int currentAirDashes;

        public float bombThreshold;
        public float BombThrowForceX;
        public float BombThrowForceY;
    }
}
