using System;
using UnityEngine;
using UnityEngine.UI;
using Utilis;

namespace UI
{


    public class OptionsUIView : MonoBehaviour
    {
        private OptionsUIController optionsUIController;

        public Toggle mechanicsToggle;
        public CanvasGroup mechanicsCanvasGroup;
        public Toggle controlToggle;
        public CanvasGroup controlsCanvasGroup;
        public Toggle gameplayToggle;
        public CanvasGroup gameplayCanvasGroup;

        public void SetController(OptionsUIController optionsUIController)
        {
            this.optionsUIController = optionsUIController;
            AddListenersToToggle();
        }

        private void AddListenersToToggle()
        {
            mechanicsToggle.onValueChanged.AddListener(OnMechanicsToggeled);
            controlToggle.onValueChanged.AddListener(OnControlsToggled);
            gameplayToggle.onValueChanged.AddListener(OnGameplayToggeled);
        }

        private void OnGameplayToggeled(bool isOn)
        {
            if (isOn)
            {
                optionsUIController.PlayButtonSound();
                CanvasGroupExtension.Show(gameplayCanvasGroup);
            }
            else
                CanvasGroupExtension.Hide(gameplayCanvasGroup);
        }

        private void OnControlsToggled(bool isOn)
        {
            if (isOn)
            {
                optionsUIController.PlayButtonSound();
                CanvasGroupExtension.Show(controlsCanvasGroup);
            }
            else
            {
                CanvasGroupExtension.Hide(controlsCanvasGroup);

            }
        }

        private void OnMechanicsToggeled(bool isOn)
        {
            if (isOn)
            {
                optionsUIController.PlayButtonSound();
                CanvasGroupExtension.Show(mechanicsCanvasGroup);
            }
            else
            {
                CanvasGroupExtension.Hide(mechanicsCanvasGroup);
            }
        }
    
    }
}
