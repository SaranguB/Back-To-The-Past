using Player;
using UnityEngine;
using Utilities;

namespace Main
{
    public class GameManager : GenericMonoSingelton<GameManager>
    {
        public PlayerService playerService;
        public TimeSwitchService timeSwitchService;

        [Header("Player")]
        [SerializeField] private PlayerView playerView;
        [SerializeField] private PlayerSO playerS0;

        protected override void Awake()
        {
            base.Awake();
            playerService = new PlayerService(playerView, playerS0);
        }
    }
}
