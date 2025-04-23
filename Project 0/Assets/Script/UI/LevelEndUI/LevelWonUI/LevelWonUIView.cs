using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI
{
    public class LevelWonUIView : BaseLevelEndUI
    {
        private LevelWonUIController levelWonUIController;
        public Button nextLevelButton;

        public void SetController(LevelWonUIController levelWonUIController)
        {
            this.levelWonUIController = levelWonUIController;
            nextLevelButton.onClick.AddListener(OnNextLevelButtonCLicked);
        }

        private void OnNextLevelButtonCLicked()
        {
            int currentScene = (SceneManager.GetActiveScene().buildIndex) + 1;
            SceneManager.LoadScene(currentScene);
        }
    }
}
