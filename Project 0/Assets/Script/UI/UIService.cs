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

        private void Start()
        {
            timeSwitchUIController = new TimeSwitchUIController(timeSwitchUIView);
            playerUIController = new PlayerUIController(playerUIView);
            InitializeUI();
        }

        private void InitializeUI()
        {
            GameManager.Instance.playerService.SetTimeSwitchUI(GetTimeSwitchUI());
            GameManager.Instance.playerService.SetPlayerUI(GetPlayerUI());
        }

        public PlayerUIController GetPlayerUI()
             => playerUIController;

        public TimeSwitchUIController GetTimeSwitchUI()
            => timeSwitchUIController;
    }
}
