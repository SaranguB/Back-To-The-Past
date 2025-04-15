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
        public float verticalInput;
        public float movementSpeed;

        //Dashing
        public float dashSpeed;
        public float dashDuration;
        public int maxDashes;
        public int currentDashes;
        public bool isDashing = false;
        public float dashTimer = 0f;
        public Vector2 inputDirection;
        public bool canPlayerAirDash = false;
        public float currentAirDashes;
        public bool canDeployBomb = true;

        public PlayerModel(PlayerSO playerS0)
        {
            this.playerSO = playerS0;

            movementSpeed = playerS0.movementSpeed;
            jumpForce = playerS0.jumpForce;
            health = playerS0.health;
            fallingSpeed = playerS0.fallingSpeed;

            dashSpeed = playerS0.dashSpeed;
            dashDuration = playerS0.dashDuration;
            maxDashes = playerS0.maxDashes;
            currentDashes = playerS0.currentDashes;
            currentAirDashes = playerS0.currentAirDashes;

        }
    }
}
