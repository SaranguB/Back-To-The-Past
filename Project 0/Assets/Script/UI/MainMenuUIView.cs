using System;
using UnityEngine;
using UnityEngine.UI;
using Utilis;

namespace UI
{
    public class MainMenuUIView : MonoBehaviour
    {
        private MainMenuUIController mainMenuUIController;

        public CanvasGroup MainMenuCanvasGroup;
        public CanvasGroup optionsCanvasGroup;
        public CanvasGroup levelSelectionMenu;

        public Button playButton;
        public Button optionsButton;
        public Button exitButton;
        public Button OptionBackButton;

        public void SetController(MainMenuUIController mainMenuUIController)
        {
            this.mainMenuUIController = mainMenuUIController;
            AddListenersToButton();
        }

        private void AddListenersToButton()
        {
            playButton.onClick.AddListener(EnableLevelSelectionMenu);
            optionsButton.onClick.AddListener(EnableOptionsMenu);
            exitButton.onClick.AddListener(exitGame);
            OptionBackButton.onClick.AddListener(DisableOptionsMenu);
        }

        private void DisableOptionsMenu()
            => CanvasGroupExtension.Hide(optionsCanvasGroup);

        private void exitGame()
            => Application.Quit();

        private void EnableOptionsMenu()
            => CanvasGroupExtension.Show(optionsCanvasGroup);

        private void EnableLevelSelectionMenu()
        {
            UnityEngine.EventSystems.EventSystem.current.SetSelectedGameObject(null);
            CanvasGroupExtension.Show(levelSelectionMenu);
        }
    }
}
