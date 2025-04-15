using Main;
using System;
using UI;
using UnityEngine;

namespace TimeSwitching
{
    public class TimeSwitchController
    {
        private TimeSwitchView timeSwitchView;
        private TimeSwitchModel timeSwitchModel;
        private TimeStateMachine timeStateMachine;

        public TimeSwitchController(TimeSwitchView timeSwitchView)
        {
            InitializeVariable(timeSwitchView);
            SetController();
            CreateStateMachine();

            SubcribeToEvents();
            SwitchTimeToPresent();

        }

        private void SwitchTimeToPresent()
        {
            ChangeTimeToPresent();
            timeSwitchView.SwitchTimeToPresent();
        }

        private void SubcribeToEvents()
        {
            GameManager.Instance.eventService.onTimeSwitched.AddListener(SwitchTime);
        }

        public void UnSubcribeToEvents()
        {
            GameManager.Instance.eventService.onTimeSwitched.RemoveListener(SwitchTime);
        }

        private void InitializeVariable(TimeSwitchView timeSwitchView)
        {
            this.timeSwitchView = timeSwitchView;
            timeSwitchModel = new TimeSwitchModel();
        }

        private void CreateStateMachine()
        {
            timeStateMachine = new TimeStateMachine(this, timeSwitchView.GetAffectedObjects());
        }

        public void SetPastProperties(TimeAffectedObjectController affectedObject)
        {
            if (affectedObject != null && affectedObject.gameObject.activeInHierarchy)
            {
                affectedObject.SetPastProperties();
            }
        }

        public void SetPresentproperties(TimeAffectedObjectController affectedObject)
        {
            if (affectedObject != null && affectedObject.gameObject.activeInHierarchy)
            {
                affectedObject.SetPresentProperties();
            }
        }

        private void SetController()
        {
            timeSwitchView.SetController(this);
        }

        public void SwitchTime()
        {
            var currentState = timeStateMachine.GetCurrentState();

            if (currentState is PastState)
            {
                ChangeTimeToPresent();
                timeSwitchView.SwitchTimeToPresent();
                
            }
            else if (currentState is PresentState)
            {
                ChangeTimeToPast();
                timeSwitchView.SwitchTimeToPast();

            }
        }

        public void ChangeTimeToPast()
        {
            ChangeTimeState(TimeState.Past);
        } 

        public void ChangeTimeToPresent()
        {
            ChangeTimeState(TimeState.Present);
        }

        public void ChangeTimeState(TimeState newState)
        {
            timeStateMachine.ChangeState(newState);
        }

     
    }
}
