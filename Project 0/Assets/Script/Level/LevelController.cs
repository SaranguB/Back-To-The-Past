using Main;
using System;
using UnityEngine;

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
            GameManager.Instance.eventService.OnPlayerFinishedLevel.AddListener(LevelFinished);
            GameManager.Instance.eventService.OnPlayerGotKey.AddListener(PlayerGotKey);
        }

        public void UnSubscribetToEvents()
        {
            GameManager.Instance.eventService.OnPlayerFinishedLevel.RemoveListener(LevelFinished);
            GameManager.Instance.eventService.OnPlayerGotKey.RemoveListener(PlayerGotKey);
        }


        private void PlayerGotKey()
        {
            levelView.PlayerGotKey();
        }


        public void LevelFinished()
        {
            levelView.LevelFInished();
        }
    }
}
