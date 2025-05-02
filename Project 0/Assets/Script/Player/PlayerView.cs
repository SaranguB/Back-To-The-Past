using System;
using System.Collections;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;
using Wepons.Bomb;
using static UnityEngine.UI.GridLayoutGroup;

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
        public CinemachineImpulseSource playerImpulseSource;

        //Jump
        [Header("Jump")]
        public Transform[] groundCheckPoint;
        public LayerMask groundLayer;

        //Bomb
        [Header("Bomb")]
        public Transform bombBagPosition;

        //Particle System
        [Header("ParticleSystem")]
        public ParticleSystem timeSwitchParticle;

        private void Start()
        {
            playerRB = GetComponent<Rigidbody2D>();
            playerTransform = playerRB.transform;
            playerStartPosition = playerTransform.position;
        }

        private void Update()
        {
            playerController.OnPlayerPositionChanged(playerTransform.position);
            playerController.HandleInput();
            playerController.UpdateState();
        }

        private void OnDestroy()
        {
            playerController.UnSubcribeToEvents();
        }

        private void FixedUpdate()
        {
            playerController.FixedUpdateState();
        }

        public void SetController(PlayerController playerController)
           => this.playerController = playerController;


        public void FlipOnDirection(float horizontalInput)
        {
            Vector3 scale = transform.localScale;
            scale.x = MathF.Sign(horizontalInput) * MathF.Abs(scale.x);
            transform.localScale = scale;
        }

        public void TakeDamage(int damage)
            => playerController.TakeDamage(damage);

        private void OnTriggerEnter2D(Collider2D other)
        {
            switch (other.tag)
            {
                case "Key":
                    if (!playerController.IsPlayerHasKey())
                        playerController.OnKeyCollected();
                    break;

                case "FinalDoor":
                    if (playerController.IsPlayerHasKey())
                        playerController.IsInfrontOfFinalDoor(true);
                    break;

                case "DeathZone":
                    playerController.TakeDamage(3);
                    break;
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            switch (other.tag)
            {
                case "FinalDoor":
                    playerController.IsInfrontOfFinalDoor(false);
                    break;
            }
        }

        public void LevelFinished()
            => StartCoroutine(LevelWon());

        public void DisableHurtAnimation()
            => playerAnimator.SetBool("IsHurt", false);

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
