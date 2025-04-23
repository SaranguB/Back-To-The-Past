using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Utilis;

namespace UI
{
    public abstract class BaseLevelEndUI : MonoBehaviour
    {
        public CanvasGroup levelEndUICanvasGroup;
        public Button replayButton;
        public Button mainMenuButton;

        private void Awake()
        {
            AddListenersToButton();
        }

        public void AddListenersToButton()
        {
            replayButton.onClick.AddListener(OnReplayButtonClicked);
            mainMenuButton.onClick.AddListener(OnMainMenuButtonClicked);
        }

        private void OnMainMenuButtonClicked()
        {
            CanvasGroupExtension.Hide(levelEndUICanvasGroup);
            SceneManager.LoadScene("MainMenu");
        }

        private void OnReplayButtonClicked()
        {
            CanvasGroupExtension.Hide(levelEndUICanvasGroup);
            Scene currentScene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(currentScene.name);
        }

        private void OnDestroy()
        {
            replayButton.onClick.RemoveListener(OnReplayButtonClicked);
            mainMenuButton.onClick.RemoveListener(OnMainMenuButtonClicked);
        }
    }
}
