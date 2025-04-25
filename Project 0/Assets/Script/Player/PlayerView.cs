using Main;
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
        public Rigidbody2D playerRB;
        public Animator playerAnimator;
        public Transform playerTransform;
        public Vector2 playerStartPosition;
        //Jump
        [Header("Jump")]
        public Transform[] groundCheckPoint;
        public LayerMask groundLayer;

        //Bomb
        [Header("Bomb")]
        public Transform bombBagPosition;

        public ParticleSystem timeSwitchParticle;

        private void Start()
        {
            playerRB = GetComponent<Rigidbody2D>();
            playerTransform = playerRB.transform;
            playerStartPosition = playerTransform.position;
        }


        private void Update()
        {
            if (playerController.GetCurrentPlayerState() is not DeadState)
            {
                playerController.OnPlayerPositionChanged(playerTransform.position);
                playerController.HandleInput();
                playerController.HandleDashing();
            }
        }

        private void FixedUpdate()
        {
            if (playerController.GetCurrentPlayerState() is not DeadState)
            {
                playerController.HandleMovement();
                playerController.ExecuteDashing();
            }
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

        public void TakeDamage(int damage)
        {
            playerController.TakeDamage(damage);
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

            if(other.CompareTag("DeathZone"))
            {
                playerController.TakeDamage(3);
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
            StartCoroutine(LevelWon());
        }

        public void ChangeStateToAlive()
            => playerController.ChangePlayerState(PlayerState.Alive);

        public void OnPlayerDead()
        {
            playerController.OnPlayerDead();
            StartCoroutine(HandlePlayerDeath());
        }

        private IEnumerator HandlePlayerDeath()
        {
            yield return new WaitForSeconds(2f);
            playerController.OnPlayerDestroyed();
            Destroy(this.gameObject);
        }

        private IEnumerator LevelWon()
        {
            yield return new WaitForSeconds(.5f);

            gameObject.SetActive(false);
            playerController.LevelFinished();
        }
    }
}
