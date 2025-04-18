using System;
using System.Collections;
using UnityEngine;
using Wepons.Bomb;

namespace Player
{
    public class PlayerView : MonoBehaviour, IDamagableFromBomb
    {
        //Player
        private PlayerController playerController;
        private Rigidbody2D playerRB;
        private Animator playerAnimator;
        private Transform playerTransform;

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
            playerTransform = playerRB.transform;

            this.playerController.SetPlayerValues(playerAnimator, playerRB);
        }


        private void Update()
        {
            playerController.OnPlayerPositionChanged(playerTransform.position);
            playerController.HandleInput();
            playerController.HandleDashing();
        }

        private void FixedUpdate()
        {
            playerController.HandleMovement();
            playerController.ExecuteDashing();
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

        public void TakeDamageFromBomb(int damage)
        {
            playerController.TakeDamageFromBomb(damage);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!playerController.IsPlayerHasKey())
            {
                if (other.CompareTag("Key"))
                {
                    playerController.OnKeyCollected();
                }
            }

            if (playerController.IsPlayerHasKey())
            {
                if (other.CompareTag("FinalDoor"))
                {
                    playerController.IsInfrontOfFinalDoor(true);
                }
            }

        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.CompareTag("FinalDoor"))
            {
                playerController.IsInfrontOfFinalDoor(false);
            }
        }

        public void LevelFinished()
        {
            StartCoroutine(DisablePlayer());
        }

        private IEnumerator DisablePlayer()
        {
            yield return new WaitForSeconds(.5f);

            gameObject.SetActive(false);
        }
    }
}
