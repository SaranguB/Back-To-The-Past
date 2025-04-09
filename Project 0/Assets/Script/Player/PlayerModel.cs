using UnityEngine;

namespace Player
{
    public class PlayerModel
    {
        private PlayerSO playerSO;

        public int health;

        [Header("Jump")]
        public bool isJumping = false;
        public float jumpForce;
        public float fallingSpeed;
        public float groundCheckDistance = .2f;

        [Header("TimeSwitching Switch")]
        public float timeSwitchingDuration;
        public float timeRequiredForSwitching = 2f;
        public bool isTimeSwitching = false;

        [Header("Move")]
        public float horizontalInput;
        public float movementSpeed;

        public PlayerModel(PlayerSO playerS0)
        {
            this.playerSO = playerS0;

            movementSpeed = playerS0.movementSpeed;
            jumpForce = playerS0.jumpForce;
            health = playerS0.health;
            fallingSpeed = playerS0.fallingSpeed;
        }
    }
}
