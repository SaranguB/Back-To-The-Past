using Main;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Level
{
    public class LevelController
    {
        private LevelView levelView;

        public LevelController(LevelView levelView)
        {
            this.levelView = levelView;
            this.levelView.SetController(this);
            SubscribeToEvents();
        }

        private void SubscribeToEvents()
        {
            GameManager.Instance.eventService.OnPlayerOpenedDoor.AddListener(LevelFinished);
            GameManager.Instance.eventService.OnPlayerGotKey.AddListener(PlayerGotKey);
        }

        public void UnSubscribetToEvents()
        {
            GameManager.Instance.eventService.OnPlayerOpenedDoor.RemoveListener(LevelFinished);
            GameManager.Instance.eventService.OnPlayerGotKey.RemoveListener(PlayerGotKey);
        }

        private void PlayerGotKey()
            =>levelView.PlayerGotKey();

        public void LevelFinished()
        {
            levelView.LevelFInished();
            UnlockNextevel();
        }

        private void UnlockNextevel()
        {
            int nextLevelBuildIndex = SceneManager.GetActiveScene().buildIndex + 1;

            if (nextLevelBuildIndex < SceneManager.sceneCountInBuildSettings)
            {
                string scenePath = SceneUtility.GetScenePathByBuildIndex(nextLevelBuildIndex);
                string sceneName = System.IO.Path.GetFileNameWithoutExtension(scenePath);

                PlayerPrefs.SetInt(sceneName, 1);
                PlayerPrefs.Save();
            }
        }
    }
}
