using Main;
using Player.UI;
using UnityEngine;

namespace UI
{
    public class UIService : MonoBehaviour
    {
        [Header("Time Switch UI")]
        private TimeSwitchUIController timeSwitchUIController;
        [SerializeField] TimeSwitchUIView timeSwitchUIView;

        [Header("Player UI")]
        private PlayerUIController playerUIController;
        [SerializeField] PlayerUIView playerUIView;

        [Header("HealthUI")]
        private HealthUIController healthUIController;
        [SerializeField] HealthUIView healthUIView;

        [Header("Main Menu")]
        private MainMenuUIController mainMenuUIController;
        [SerializeField] private MainMenuUIView mainMenuUIView;

        [Header("LevelSelectionUI")]
        private LevelSelectionUIController levelSelectionUIController;
        [SerializeField] private LevelSelectionUIView levelSelectionUIView;

        [Header("LevelWonUI")]
        private LevelWonUIController levelWonUIController;
        [SerializeField] private LevelWonUIView levelWonUIView;

        [Header("LevelLostUI")]
        private LevelLostUIController levelLostUIController;
        [SerializeField] private LevelLostUIView levelLostUIView;

        [Header("Options")]
        private OptionsUIController optionsUIController;
        [SerializeField] private OptionsUIView optionsUIView;

        private void Start()
        {
            InitializeUIControllers();
            RegisterUI();
            SubscribeToEvents();
        }

        private void SubscribeToEvents()
        {
            if (levelLostUIView != null && levelWonUIView != null)
            {
                GameManager.Instance.eventService.OnPlayerFinishedLevel.AddListener(CreateLevelWonUI);
                GameManager.Instance.eventService.OnPlayerDead.AddListener(CreateLevelLostUI);
            }
        }

        private void OnDestroy()
        {
            UnSubscribeToEvents();
        }

        private void UnSubscribeToEvents()
        {
            if (levelLostUIView != null && levelWonUIView != null)
            {
                GameManager.Instance.eventService.OnPlayerFinishedLevel.RemoveListener(CreateLevelWonUI);
                GameManager.Instance.eventService.OnPlayerDead.RemoveListener(CreateLevelLostUI);
            }
        }

        private void InitializeUIControllers()
        {
            if(optionsUIView != null)
                optionsUIController = new OptionsUIController(optionsUIView);

            if (mainMenuUIView != null)
                mainMenuUIController = new MainMenuUIController(mainMenuUIView);

            if (levelSelectionUIView != null)
                levelSelectionUIController = new LevelSelectionUIController(levelSelectionUIView);

            if (timeSwitchUIView != null)
                timeSwitchUIController = new TimeSwitchUIController(timeSwitchUIView);

            if (playerUIView != null)
                playerUIController = new PlayerUIController(playerUIView);

            if (healthUIView != null)
                healthUIController = new HealthUIController(healthUIView);
        }

        private void RegisterUI()
        {
            if (timeSwitchUIView != null && playerUIView != null && healthUIView != null)
                GameManager.Instance.playerService.SetUI(GetTimeSwitchUI(), GetPlayerUI(), GetHealthUI());
        }

        public void CreateLevelLostUI()
           => levelLostUIController = new LevelLostUIController(levelLostUIView);

        public void CreateLevelWonUI()
        {
            GameManager.Instance.soundService.StopBackgroundSong();
            GameManager.Instance.soundService.PlaySoundEffects(Audio.SoundType.LevelWonSound);
            levelWonUIController = new LevelWonUIController(levelWonUIView);
        }

        public PlayerUIController GetPlayerUI()
             => playerUIController;

        public TimeSwitchUIController GetTimeSwitchUI()
            => timeSwitchUIController;

        public HealthUIController GetHealthUI()
             => healthUIController;
    }
}
