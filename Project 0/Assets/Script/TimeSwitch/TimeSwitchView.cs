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
        }

        public TimeAffectedObjectController[] GetAffectedObjects()
            => objectController;

    }
}
