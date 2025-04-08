using Main;
using System;
using UnityEngine;

namespace UI
{
    public class UIService : MonoBehaviour
    {
        [Header("Time Switch UI")]
        private TimeSwitchUIController timeSwitchUIController;
        [SerializeField] TimeSwitchUIView timeSwitchUIView;

        private void Start()
        {
            timeSwitchUIController = new TimeSwitchUIController(timeSwitchUIView);
            RegisterTimeSwitchUI();
        }

        private void RegisterTimeSwitchUI()
        {
            GameManager.Instance.playerService.SetTimeSwitchUI(GetTimeSwitchUI());
        }

        public TimeSwitchUIController GetTimeSwitchUI()
            => timeSwitchUIController;
    }
}
