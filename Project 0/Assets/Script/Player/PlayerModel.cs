using UnityEngine;

namespace Player
{
    public class PlayerModel
    {
        private PlayerSO playerSO;

        //Health
        public int numberOfLives;
        public int currentLives;

        //Jump
        public bool isJumping = false;
        public float jumpForce;
        public float fallingSpeed;
        public float groundCheckDistance = .2f;

        //TimeSwitching Switch
        public float timeSwitchingDuration;
        public float timeRequiredForSwitching = 2f;
        public bool isTimeSwitching = false;

        //Move
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
        public bool canPlayerAirDash = true;
        public float currentAirDashes;
        public bool canDeployBomb = true;

        //Bomb
        public bool isHoldingBombKey = false;
        public float bombHoldTimer = 0f;
        public float bombThreshold;
        public float bombThrowForceX;
        public float bombThrowForceY;

        //Key
        public bool isPlayerHasKey = false;
        public bool canUnlockDoor = true;

        public PlayerModel(PlayerSO playerS0)
        {
            this.playerSO = playerS0;

            movementSpeed = playerS0.movementSpeed;
            jumpForce = playerS0.jumpForce;
            fallingSpeed = playerS0.fallingSpeed;

            numberOfLives = playerS0.numberOfLives;
            currentLives = numberOfLives;

            dashSpeed = playerS0.dashSpeed;
            dashDuration = playerS0.dashDuration;
            maxDashes = playerS0.maxDashes;
            currentDashes = playerS0.currentDashes;
            currentAirDashes = playerS0.currentAirDashes;

            bombThreshold = playerS0.bombThreshold;
            bombThrowForceX = playerS0.BombThrowForceX;
            bombThrowForceY = playerS0.BombThrowForceY;
        }
    }
}
