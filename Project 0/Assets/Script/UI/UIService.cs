using Main;
using Player.UI;
using System;
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

        [Header("LevelSelectionUI")]
        private LevelSelectionUIController levelSelectionUIController;
        [SerializeField] private LevelSelectionUIView levelSelectionUIView;

        private void Start()
        {
            if (levelSelectionUIView != null)
                levelSelectionUIController = new LevelSelectionUIController(levelSelectionUIView);

            if (timeSwitchUIView != null)
                timeSwitchUIController = new TimeSwitchUIController(timeSwitchUIView);

            if (playerUIView != null)
                playerUIController = new PlayerUIController(playerUIView);

            if (healthUIView != null)
                healthUIController = new HealthUIController(healthUIView);

            InitializeUI();
        }

        private void InitializeUI()
        {
            if (timeSwitchUIView != null && playerUIView != null && healthUIView != null)
                GameManager.Instance.playerService.SetUI(GetTimeSwitchUI(), GetPlayerUI(), GetHealthUI());
        }

        public PlayerUIController GetPlayerUI()
             => playerUIController;

        public TimeSwitchUIController GetTimeSwitchUI()
            => timeSwitchUIController;

        public HealthUIController GetHealthUI()
             => healthUIController;
    }
}
