using UnityEngine;

namespace UI
{
    public class LevelSelectionUIController
    {
        private LevelSelectionUIView levelSelectionUIView;

        public LevelSelectionUIController(LevelSelectionUIView levelSelectionUIView)
        {
            this.levelSelectionUIView = levelSelectionUIView;
            this.levelSelectionUIView.SetController(this);
        }

        public void UnlockLevel(string levelName)
        {
            Debug.Log($"Saving level: {levelName}");
            PlayerPrefs.SetInt(levelName, 1);
            PlayerPrefs.Save();
        }

        public bool IsLevelUnlocked(string levelName)
            =>PlayerPrefs.GetInt(levelName, 0) == 1;
    }
}
