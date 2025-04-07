using System;
using UnityEngine;
using TimeSwitching;

namespace Player
{
    public class PlayerView : MonoBehaviour
    {
        private PlayerController playerController;
        private PlayerState playerState;
        private Rigidbody2D playerRB;

        [Header("Jump")]
        private bool isJumping = false;
        [SerializeField] private Transform[] groundCheckPoint;
        [SerializeField] private float groundCheckDistance;
        [SerializeField] private LayerMask groundLayer;

        [Header("TimeSwitching Switch")]
        
        private float timeSwitchingDuration;
        private float timeRequiredForSwitching = 2f;
        private bool isTimeSwitching = false;

        private float horizontalInput;

        private void Start()
        {

            playerRB = GetComponent<Rigidbody2D>();
            playerState = PlayerState.ALIVE;

            playerController.SetPlayerRb(playerRB);
        }


        private void Update()
        {
            if (playerState == PlayerState.ALIVE)
            {
                SetMoveInput();
                SetJumpInput();
                SetTimeSwitchInput();
            }
        }

        private void SetTimeSwitchInput()
        {
            if (Input.GetKey(KeyCode.Tab))
            {
                if (!isTimeSwitching)
                {
                    timeSwitchingDuration += Time.deltaTime;
                    
                    if (timeSwitchingDuration >= timeRequiredForSwitching)
                    {
                        Debug.Log("Time Switched");
                        playerController.SwitchTime();
                        isTimeSwitching = true;
                    }
                }
            }

            if (Input.GetKeyUp(KeyCode.Tab))
            {
                timeSwitchingDuration = 0f;
                isTimeSwitching = false;
            }
        }

        private void SetJumpInput()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                isJumping = true;
            }
        }

        private void SetMoveInput()
        {
            horizontalInput = Input.GetAxis("Horizontal");
        }

        private void FixedUpdate()
        {
            if (playerState == PlayerState.ALIVE)
            {
                playerController.Move(horizontalInput);

                if (isJumping && ISGrounded())
                {
                    playerController.Jump();
                    isJumping = false;
                }
                playerController.HandleFalling();

            }
        }

        private bool ISGrounded()
          => playerController.IsGrounded(groundCheckPoint, groundCheckDistance, groundLayer);


        public void SetController(PlayerController playerController)
        {
            this.playerController = playerController;
        }

        public Rigidbody2D GetPlayerRigidBody()
            => playerRB;

        public void FlipOnDirection(float horizontalInput)
        {
            if (horizontalInput != 0)
            {
                Vector3 scale = transform.localScale;
                scale.x = MathF.Sign(horizontalInput) * MathF.Abs(scale.x);
                transform.localScale = scale;
            }
        }
    }
}
