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
        private Animator playerAnimator;

        //Jump
        [Header("Jump")]
        public Transform[] groundCheckPoint;
        public LayerMask groundLayer;

        //Bomb
        [Header("Bomb")]
        public Transform bombBagPosition;

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

        public void SetController(PlayerController playerController)
        {
            this.playerController = playerController;
        }

        public void FlipOnDirection(float horizontalInput)
        {
            Vector3 scale = transform.localScale;
            scale.x = MathF.Sign(horizontalInput) * MathF.Abs(scale.x);
            transform.localScale = scale;
        }

    }
}
