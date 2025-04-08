using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class TimeSwitchUIController
    {
        private TimeSwitchUIView timeSwitchUIView;
        private Image timeSwitchRingImage;
        private float currentFillTime;
        public TimeSwitchUIController(TimeSwitchUIView timeSwitchUIView)
        {
            this.timeSwitchUIView = timeSwitchUIView;

            timeSwitchRingImage = this.timeSwitchUIView.filleImage;
            ResetUI();
        }

        public void UpdateTimeSwitchUISlider(bool isKeyHeld, float fillDuration)
        {
            if (isKeyHeld)
            {
                currentFillTime += Time.deltaTime;
                timeSwitchRingImage.fillAmount = currentFillTime / fillDuration;

                if (currentFillTime >= fillDuration)
                {
                    timeSwitchRingImage.fillAmount = 0;
                }
            }
            else
            {
                ResetUI();
            }
        }

        private void ResetUI()
        {
            currentFillTime = 0f;
            timeSwitchRingImage.fillAmount = 0f;

        }


    }
}