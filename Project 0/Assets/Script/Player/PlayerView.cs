using System;
using UnityEngine;
using TimeSwitching;
using UI;
using static UnityEngine.Rendering.DebugUI;

namespace Player
{
    public class PlayerView : MonoBehaviour
    {
        //Player
        private PlayerController playerController;
        private Rigidbody2D playerRB;

        //Jump
        [Header("Jump")]
        [SerializeField] private Transform[] groundCheckPoint;
        [SerializeField] private float groundCheckDistance;
        [SerializeField] private LayerMask groundLayer;

        //Animation
        private Animator playerAnimator;

        private void Start()
        {

            playerRB = GetComponent<Rigidbody2D>();
            playerAnimator = GetComponent<Animator>();

            this.playerController.SetPlayerValues(playerAnimator, playerRB);
        }


        private void Update()
        {
            playerController.HandleInput();
        }

        private void FixedUpdate()
        {
            playerController.HandleMovement();
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
