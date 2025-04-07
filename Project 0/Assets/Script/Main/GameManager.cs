using Player;
using UnityEngine;
using Utilities;
using Time;

namespace Main
{
    public class GameManager : GenericMonoSingelton<GameManager>
    {
        public PlayerService playerService;
        public TimeSwitchService timeSwitchService;

        [Header("Player")]
        [SerializeField] private PlayerView playerView;
        [SerializeField] private PlayerSO playerS0;

        [Header("TimeSwitch")]
        [SerializeField] private TimeSwitchView timeSwitchView;

        protected override void Awake()
        {
            base.Awake();
            playerService = new PlayerService(playerView, playerS0);
            timeSwitchService = new TimeSwitchService(timeSwitchView);
        }
    }
}
