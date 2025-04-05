using UnityEngine;

namespace Player
{
    public class PlayerModel
    {
        private PlayerSO playerSO;

        public float movementSpeed;
        public float jumpForce;
        public int health;
        public float fallingSpeed;

        public PlayerModel(PlayerSO playerS0)
        {
            this.playerSO = playerS0;

            movementSpeed = playerS0.movementSpeed;
            jumpForce = playerS0.jumpForce;
            health = playerS0.health;
            fallingSpeed = playerS0.fallingSpeed;
        }
    }
}
