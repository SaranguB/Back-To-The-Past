using UnityEngine;

namespace Player
{
    public class PlayerService
    {
        private PlayerController playerController;
        public PlayerService(PlayerView playerView, PlayerSO playerS0) 
        { 
            playerController = new PlayerController(playerView, playerS0);
        }

        public PlayerController GetPlayer() => playerController;
      
    }
}
