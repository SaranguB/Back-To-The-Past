using System;
using UnityEngine;
using TimeSwitching;
using UI;

namespace Player
{
    public class PlayerView : MonoBehaviour
    {
        private PlayerController playerController;
        private PlayerState playerState;
        private Rigidbody2D playerRB;

        [Header("Jump")]
        [SerializeField] private Transform[] groundCheckPoint;
        [SerializeField] private float groundCheckDistance;
        [SerializeField] private LayerMask groundLayer;

        [Header("Animation")]
        [SerializeField] private Animator playerAnimator;

        private void Start()
        {

            playerRB = GetComponent<Rigidbody2D>();
            playerState = PlayerState.ALIVE;
            playerAnimator = GetComponent<Animator>();

            this.playerController.SetPlayerValues(playerAnimator, playerRB);
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
                if (!playerController.GetIsTimeSwitching())
                {
                    playerController.CancelTimeSwitching(Time.deltaTime, playerAnimator);
                }
                else
                {
                    playerController.StopPlayerMovement(playerAnimator);
                }
            }

            if (Input.GetKeyUp(KeyCode.Tab))
            {
                playerController.handleTimeSwitching();
            }
        }

        private void SetJumpInput()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                playerController.SetJumping(true);
            }
        }

        private void SetMoveInput()
        {
            playerController.SetMoveInputValues(playerAnimator, Input.GetAxis("Horizontal"));
        }

        private void FixedUpdate()
        {
            if (playerState == PlayerState.ALIVE && !playerController.GetIsTimeSwitching())
            {
                playerController.HandleMovement();
            }
        }

        public bool ISGrounded()
          => playerController.IsGrounded(groundCheckPoint, groundCheckDistance, groundLayer);


        public void SetController(PlayerController playerController)
        {
            this.playerController = playerController;
            
        }

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
