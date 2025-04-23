using UnityEngine;
using Utilis;

namespace UI
{
    public class LevelLostUIController
    {
        private LevelLostUIView levelLostUIView;

        public LevelLostUIController(LevelLostUIView levelLostUIView)
        {
            this.levelLostUIView = levelLostUIView;
            this.levelLostUIView.SetController(this);

            EnableLevelLostUI();
        }

        public void EnableLevelLostUI()
            => CanvasGroupExtension.Show(levelLostUIView.levelEndUICanvasGroup);
    }
}
