using Player.UI;
using UI;
using Wepons.Bomb;

namespace Player
{
    public class PlayerService
    {
        private PlayerController playerController;
        private BombPool bombPool;

        public PlayerService(PlayerView playerView, PlayerSO playerS0, BombSO bombSO, BombView bombPrefab)
        {
            bombPool = new BombPool(bombSO, bombPrefab);
            playerController = new PlayerController(playerView, playerS0, bombPool);
        }

        public void SetUI(TimeSwitchUIController timeSwitchUIController, PlayerUIController playerUIController, HealthUIController healthUIController)
        {
            SetTimeSwitchUI(timeSwitchUIController);
            SetPlayerUI(playerUIController);
            SetHealthUI(healthUIController);
        }

        public void SetTimeSwitchUI(TimeSwitchUIController timeSwitchUIController)
            => playerController.SetTimeSwitchUI(timeSwitchUIController);

        public void ReturneBombToPool(BombController bombToReturn)
            => bombPool.ReturnItem(bombToReturn);

        public void SetPlayerUI(PlayerUIController playerUIController)
           => playerController.SetPlayerUI(playerUIController);

        public void SetHealthUI(HealthUIController healthUIController)
           => playerController.SetHealthUI(healthUIController);

        public PlayerController GetPlayer()
            => playerController;
    }
}
