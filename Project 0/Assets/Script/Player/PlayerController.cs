using System;
using UnityEngine;

namespace Player
{
    public class PlayerController
    {
        private PlayerView playerView;
        private PlayerModel playerModel;
        private Rigidbody2D playerRB;

        

        public PlayerController(PlayerView playerView, PlayerSO playerS0)
        {
            this.playerView = playerView;
            playerModel = new PlayerModel(playerS0);

            this.playerView.SetController(this);
        }

        public void Move(float horizontalInput)
        {
            float speed = horizontalInput * playerModel.movementSpeed;
            Vector2 currentvelocity = playerRB.linearVelocity;
            playerRB.linearVelocity = new Vector2(speed, currentvelocity.y);

            playerView.FlipOnDirection(horizontalInput);
        }

        public void SetPlayerRb(Rigidbody2D playerRB)
                => this.playerRB = playerRB;

        public void Jump()
        {
            playerRB.linearVelocity = new Vector2(playerRB.linearVelocityX, playerModel.jumpForce);
        }

        public bool IsGrounded(Transform[] groundCheckPoint, float groundCheckDistance, LayerMask groundLayer)
        {
            foreach (Transform point in groundCheckPoint)
            {
                if (Physics2D.OverlapCircle(point.position, groundCheckDistance, groundLayer))
                    return true;
            }
            return false;
        }

        public void HandleFalling()
        {
            if (playerRB.linearVelocity.y < 0)
            {
                playerRB.gravityScale = playerModel.fallingSpeed;
            }
            else if(playerRB.linearVelocity.y>0 && !Input.GetKey(KeyCode.Space))
            {
                playerRB.gravityScale = playerModel.fallingSpeed;
            }
            else
            {
                playerRB.gravityScale = 1f; 
            }
        }
    }
}
