using UnityEngine;
using Utilis;

namespace UI
{
    public class LevelWonUIController
    {
        private LevelWonUIView levelWonUIView;

        public LevelWonUIController(LevelWonUIView levelWonUIView)
        {
            this.levelWonUIView = levelWonUIView;
            this.levelWonUIView.SetController(this);
            EnableLevelWonUI();
        }

        public void EnableLevelWonUI()
           => CanvasGroupExtension.Show(levelWonUIView.levelEndUICanvasGroup);
    }
}
