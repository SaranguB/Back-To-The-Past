using UnityEngine;

namespace Player
{
    [CreateAssetMenu(fileName ="Player", menuName = "ScriptableObjects/player")]
    public class PlayerSO : ScriptableObject    
    {
        [Header("Health")]
        public int numberOfLives;

        [Header("Movement")]
        public float movementSpeed;

        [Header("Jump")]
        public float jumpForce;
        public float fallingSpeed;

        [Header("Dash")]
        public float dashSpeed;
        public float dashDuration;
        public int maxDashes;
        public int currentDashes;
        public int currentAirDashes;

        [Header("Bomb Values")]
        public float bombThreshold;
        public float BombThrowForceX;
        public float BombThrowForceY;
    }
}
