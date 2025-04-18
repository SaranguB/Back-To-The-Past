using Events;
using Main;
using System;
using UnityEngine;

namespace Objects.Destroyable
{
    public class DestroyableObjectController
    {
        private DestroyableObjectView destroyableObjectView;
        private bool isPresent = true;

        public DestroyableObjectController(DestroyableObjectView destroyableObjectView)
        {
            this.destroyableObjectView = destroyableObjectView;
            this.destroyableObjectView.SetController(this);
            SubscribeToEvents();
        }

        private void SubscribeToEvents()
        {
            GameManager.Instance.eventService.OnTimeSwitchWithBoolParam.AddListener(TimeSwitched);
        }

        private void TimeSwitched(bool value)
        {
            isPresent = value;

            if (isPresent)
            {
                destroyableObjectView.TimeSwitchedToPresent();
            }
            else
            {
                destroyableObjectView.TimeSwitchedToPast();
            }
        }

        public bool GetIsPresentTime()
            => isPresent;
    }
}
