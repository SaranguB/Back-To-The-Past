using Player;
using System;
using UI;
using UnityEngine;

namespace TimeSwitching
{
    public class TimeSwitchView : MonoBehaviour
    {
        [SerializeField] private TimeAffectedObjectController[] objectController;

        [SerializeField] private GameObject pastPlatform;
        [SerializeField] private GameObject presentPlatform;
        [SerializeField] private GameObject pastBackgroundImage;
        [SerializeField] private GameObject presentBackgroundImage;

        private TimeSwitchController timeSwitchController;


        private void OnDisable()
        {
            timeSwitchController.UnSubcribeToEvents();
        }


        public void SetController(TimeSwitchController timeSwitchController)
        {
            this.timeSwitchController = timeSwitchController;
        }

        public TimeAffectedObjectController[] GetAffectedObjects()
            => objectController;

        public void SwitchTimeToPresent()
        {
            EnablePresentPlatform();
            DisablePastPlatform();
        }

        public void SwitchTimeToPast()
        {
            EnablePastPlatform();
            DisablePresentPlatform();

        }

        private void DisablePastPlatform()
        {
            pastPlatform.SetActive(false);
            pastBackgroundImage.SetActive(false);
        }

        private void EnablePastPlatform()
        {
            pastPlatform.SetActive(true);
            pastBackgroundImage.SetActive(true);
        }

        private void DisablePresentPlatform()
        {
            presentPlatform.SetActive(false);
            presentBackgroundImage.SetActive(false);

        }

        private void EnablePresentPlatform()
        {
            presentPlatform.SetActive(true);
            presentBackgroundImage.SetActive(true);

        }



    }
}
