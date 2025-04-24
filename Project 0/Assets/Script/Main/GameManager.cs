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
using UI;
using Audio;
using UnityEngine.SceneManagement;

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
        public UIService uiService;
        public SoundService soundService;

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

        [Header("Audio")]
        [SerializeField] private SoundSO soundSo;
        [SerializeField] private AudioSource audioEffectSource;
        [SerializeField] private AudioSource backgroundMusicSource;

        public object OnPlayerGotAttackedByEnemy { get; internal set; }

        protected override void Awake()
        {
            base.Awake();
            InitializeServices();
            InitializeDestroyabelObjectController();
        }

        private void Start()
        {
            PlayBackgroundMusic();
        }

        private void InitializeServices()
        {
            soundService = new SoundService(soundSo, audioEffectSource, backgroundMusicSource);
            eventService = new EventService();

            if (levelView != null)
                levelService = new LevelService(levelView);

            if (vfxPrefab != null)
                vfxService = new VFXService(vfxPrefab);

            if (timeSwitchView != null)
                timeSwitchService = new TimeSwitchService(timeSwitchView);

            if (trapViewCollection != null)
                trapService = new TrapService(trapViewCollection);

            if (playerView != null)
                playerService = new PlayerService(playerView, playerS0, bombSO, bombView);

            if (enemyViewCollection != null)
                enemyService = new EnemyService(enemyViewCollection, bombSO, bombView);
        }

        private void PlayBackgroundMusic()
        {
            if (SceneManager.GetActiveScene().name == "MainMenu")
                soundService.PlayBackgroundMusic(SoundType.MenuBackground);
            else
                soundService.PlayBackgroundMusic(SoundType.GameplayBackground);
        }

        private void InitializeDestroyabelObjectController()
        {
            if (destroyableObjectViewCollection != null)
                foreach (DestroyableObjectView destroyableObjectView in destroyableObjectViewCollection.destroyableObjectViews)
                {
                    DestroyableObjectController destroyableObjectController = new DestroyableObjectController(destroyableObjectView);
                }
        }
    }
}
