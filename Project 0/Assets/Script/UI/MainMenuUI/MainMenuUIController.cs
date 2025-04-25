using Audio;
using Main;
using System;
using UnityEngine;

namespace UI
{
    public class MainMenuUIController
    {
        private MainMenuUIView mainMenuUIView;

        public MainMenuUIController(MainMenuUIView mainMenuUIView)
        {
            this.mainMenuUIView = mainMenuUIView;
            this.mainMenuUIView.SetController(this);
        }

        public void PlayButtonSound()
        {
            GameManager.Instance.soundService.PlaySoundEffects(SoundType.ButtonSound);
        }
    }
}
