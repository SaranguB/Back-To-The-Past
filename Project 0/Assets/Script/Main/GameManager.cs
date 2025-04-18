using Player;
using UnityEngine;
using Utilities;
using TimeSwitching;
using Events;
using Wepons.Bomb;
using Enemy;
using System;
using Objects.Destroyable;
using Level;

namespace Main
{
    public class GameManager : GenericMonoSingelton<GameManager>
    {
        public PlayerService playerService;
        public TimeSwitchService timeSwitchService;
        public EventService eventService;
        public EnemyService enemyService;
        public LevelService levelService;

        [Header("Player")]
        [SerializeField] private PlayerView playerView;
        [SerializeField] private PlayerSO playerS0;

        [Header("TimeSwitch")]
        [SerializeField] private TimeSwitchView timeSwitchView;

        [Header("Wepons")]
        [SerializeField] private BombView bombView;
        [SerializeField] private BombSO bombSO;

        [Header("Enemy")]
        [SerializeField] private EnemyViewCollection enemyViewCollection;

        [Header("Destroyable Objects")]
        [SerializeField] private DestroyableObjectViewCollection destroyableObjectViewCollection;

        [Header("Level")]
        [SerializeField] private LevelView levelView;

        protected override void Awake()
        {
            base.Awake();
            eventService = new EventService();
            levelService = new LevelService(levelView);
            timeSwitchService = new TimeSwitchService(timeSwitchView);
            playerService = new PlayerService(playerView, playerS0, bombSO, bombView);
            enemyService = new EnemyService(enemyViewCollection, bombSO, bombView);

            InitializeDestroyabelObjectController();
        }

        private void InitializeDestroyabelObjectController()
        {
            foreach (DestroyableObjectView destroyableObjectView in destroyableObjectViewCollection.destroyableObjectViews)
            {
                DestroyableObjectController destroyableObjectController = new DestroyableObjectController(destroyableObjectView);
            }
        }
    }
}
