using System;
using UnityEngine;

namespace Player
{
    public class PlayerView : MonoBehaviour
    {
        private PlayerController playerController;
        private PlayerState playerState;
        private Rigidbody2D playerRB;

        [Header("Jump")]
        private bool isJump;
        [SerializeField] private Transform[] groundCheckPoint;
        [SerializeField] private float groundCheckDistance;
        [SerializeField] private LayerMask groundLayer;

        private float horizontalInput;

        private void Start()
        {
            isJump = false;
            playerRB = GetComponent<Rigidbody2D>();
            playerState = PlayerState.ALIVE;

            playerController.SetPlayerRb(playerRB);
        }

        private void Update()
        {
            if (playerState == PlayerState.ALIVE)
            {
                SetMoveValue();
                SetJumpvalue();
            }
        }

        private void SetJumpvalue()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                isJump = true;
            }
        }

        private void SetMoveValue()
        {
            horizontalInput = Input.GetAxis("Horizontal");
        }

        private void FixedUpdate()
        {
            if (playerState == PlayerState.ALIVE)
            {
                playerController.Move(horizontalInput);

                if (isJump && ISGrounded())
                {
                    playerController.Jump();
                    isJump = false;
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
