using UnityEngine;

namespace Time
{
    public class TimeSwitchView : MonoBehaviour
    {
        [SerializeField] private TimeAffectedObjectController[] objectController;

        private TimeSwitchController timeSwitchController;

        public void SetController(TimeSwitchController timeSwitchController)
        {
            this.timeSwitchController = timeSwitchController;
            SetPresentproperties();
        }

        public void SetPastProperties()
        {
            foreach (TimeAffectedObjectController affectedObject in objectController)
            {
                timeSwitchController.SetPastProperties(affectedObject);
            }
        }

        public void SetPresentproperties()
        {
            foreach (TimeAffectedObjectController affectedObject in objectController)
            {
                timeSwitchController.SetPresentproperties(affectedObject);
            }
        }


    }
}
