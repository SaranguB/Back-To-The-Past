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
            Debug.Log("Switched");
            EnablePastPlatform();
            DisablePresentPlatform();

        }

        private void DisablePastPlatform()
        {
            pastPlatform.SetActive(false);
        }

        private void EnablePastPlatform()
        {
           pastPlatform.SetActive(true);
        }

        private void DisablePresentPlatform()
        {
            presentPlatform.SetActive(false);
        }

        private void EnablePresentPlatform()
        {
            presentPlatform.SetActive(true);
        }


      
    }
}
