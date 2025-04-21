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
using Trap;
using VFX;

namespace Main
{
    public class GameManager : GenericMonoSingelton<GameManager>
    {
        public PlayerService playerService;
        public TimeSwitchService timeSwitchService;
        public EventService eventService;
        public EnemyService enemyService;
        public LevelService levelService;
        public TrapService trapService;
        public VFXService vfxService;

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

        [Header("Traps")]
        [SerializeField] private TrapViewCollection trapViewCollection;

        [Header("VFX")]
        [SerializeField] private VFXView vfxPrefab;

        public object OnPlayerGotAttackedByEnemy { get; internal set; }

        protected override void Awake()
        {
            base.Awake();
            InitializeServices();
            InitializeDestroyabelObjectController();
        }

        private void InitializeServices()
        {
            eventService = new EventService();
            levelService = new LevelService(levelView);
            vfxService = new VFXService(vfxPrefab);
            timeSwitchService = new TimeSwitchService(timeSwitchView);

            if (trapViewCollection != null)
                trapService = new TrapService(trapViewCollection);

            playerService = new PlayerService(playerView, playerS0, bombSO, bombView);
            enemyService = new EnemyService(enemyViewCollection, bombSO, bombView);
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
