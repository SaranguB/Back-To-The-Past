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

        private void Start()
        {
            timeSwitchUIController = new TimeSwitchUIController(timeSwitchUIView);
            playerUIController = new PlayerUIController(playerUIView);
            healthUIController = new HealthUIController(healthUIView);
            InitializeUI();
        }

        private void InitializeUI()
        {
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
