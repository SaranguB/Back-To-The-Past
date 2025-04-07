using UnityEngine;

namespace Time
{
    public class TimeSwitchView : MonoBehaviour
    {
        private TimeSwitchController timeSwitchController;
        public void SetController(TimeSwitchController timeSwitchController)
        {
            this.timeSwitchController = timeSwitchController;
        }
    }
}
