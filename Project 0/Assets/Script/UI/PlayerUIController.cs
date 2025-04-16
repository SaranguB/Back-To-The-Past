using System;
using UnityEngine;
using UnityEngine.UI;

namespace Player.UI
{
    public class PlayerUIController
    {
        private Image bombThrowFillImage;
        private float currentbombThrowFillTimer;
        private PlayerUIView playerUIView;
        public PlayerUIController(PlayerUIView playerUIView)
        {
            this.playerUIView = playerUIView;
            bombThrowFillImage = this.playerUIView.bombThrowFillImage;

            ResetUI();
        }

        public void UpdateBombThrowUISlider(bool isKeyHeld, float fillDuration)
        {
            if (isKeyHeld)
            {
                currentbombThrowFillTimer += Time.deltaTime;
                bombThrowFillImage.fillAmount = currentbombThrowFillTimer / fillDuration;

                if (currentbombThrowFillTimer >= fillDuration)
                {
                    bombThrowFillImage.fillAmount = 1;
                }
            }
            else
            {
                ResetUI();
            }
        }

        public void EnableBombThrowChargingBar(bool value)
        {
            playerUIView.bombThrowChargingBar.gameObject.SetActive(value);
        }

        public void ResetUI()
        {
            currentbombThrowFillTimer = 0f;
            bombThrowFillImage.fillAmount = 0f;
            EnableBombThrowChargingBar(false);


        }
    }
}
