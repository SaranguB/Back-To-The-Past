using Player;
using UnityEngine;
using Utilities;
using TimeSwitching;
using Events;
using Wepons.Bomb;

namespace Main
{
    public class GameManager : GenericMonoSingelton<GameManager>
    {
        public PlayerService playerService;
        public TimeSwitchService timeSwitchService;
        public EventService eventService;

        [Header("Player")]
        [SerializeField] private PlayerView playerView;
        [SerializeField] private PlayerSO playerS0;

        [Header("TimeSwitch")]
        [SerializeField] private TimeSwitchView timeSwitchView;

        [Header("Wepons")]
        [SerializeField] private BombView bombView;
        [SerializeField] private BombSO bombSO;

        protected override void Awake()
        {
            base.Awake();
            eventService = new EventService();
            playerService = new PlayerService(playerView, playerS0, bombSO, bombView);
            timeSwitchService = new TimeSwitchService(timeSwitchView);
        }
    }
}
