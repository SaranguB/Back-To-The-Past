using System.Collections;
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

        private void Start()
        {
            playButton.interactable = false;
            optionsButton.interactable = false;
            exitButton.interactable = false;

            StartCoroutine(EnableButtonsAfterDelay());
        }

        private IEnumerator EnableButtonsAfterDelay()
        {
            yield return new WaitForSeconds(0.1f);
            playButton.interactable = true;
            optionsButton.interactable = true;
            exitButton.interactable = true;
        }

        public void SetController(MainMenuUIController mainMenuUIController)
        {
            this.mainMenuUIController = mainMenuUIController;
            AddListenersToButton();
        }

        private void AddListenersToButton()
        {
            playButton.onClick.AddListener(OnPlayButtonClicked);
            optionsButton.onClick.AddListener(OnOptionsButtonClicked);
            exitButton.onClick.AddListener(OnExitButtonClicked);
            OptionBackButton.onClick.AddListener(OnOptionBackButtonClicked);
        }

        private void OnOptionBackButtonClicked()
        {
            mainMenuUIController.PlayButtonSound();
            CanvasGroupExtension.Hide(optionsCanvasGroup);
        }

        private void OnExitButtonClicked()
        {
            mainMenuUIController.PlayButtonSound();
            Application.Quit();
        }

        private void OnOptionsButtonClicked()
        {
            mainMenuUIController.PlayButtonSound();
            CanvasGroupExtension.Show(optionsCanvasGroup);
        }

        private void OnPlayButtonClicked()
        {
            mainMenuUIController.PlayButtonSound();
            UnityEngine.EventSystems.EventSystem.current.SetSelectedGameObject(null);
            CanvasGroupExtension.Show(levelSelectionMenu);

        }
    }
}
