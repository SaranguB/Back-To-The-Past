using Main;
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
            CanvasGroupExtension.Hide(levelEndUICanvasGroup);
        }

        public void AddListenersToButton()
        {
            replayButton.onClick.AddListener(OnReplayButtonClicked);
            mainMenuButton.onClick.AddListener(OnMainMenuButtonClicked);
        }

        private void OnMainMenuButtonClicked()
        {
            PlayButtonSound();
            SceneManager.LoadScene("MainMenu");
        }

        private void OnReplayButtonClicked()
        {
            PlayButtonSound();
            Scene currentScene = SceneManager.GetActiveScene();
            SceneManager.LoadSceneAsync(currentScene.name);
        }

        public static void PlayButtonSound()
           =>GameManager.Instance.soundService.PlaySoundEffects(Audio.SoundType.ButtonSound);
        

        private void OnDestroy()
        {
            replayButton.onClick.RemoveListener(OnReplayButtonClicked);
            mainMenuButton.onClick.RemoveListener(OnMainMenuButtonClicked);
        }
    }
}
