using Events;
using UnityEngine;


namespace Events
{
    public class EventService
    {
        public EventController onTimeSwitched;
        public EventController<Vector2> onPlayerPositionChanged;
        public EventController<bool> OnTimeSwitchWithBoolParam;

        public EventService()
        {
            onTimeSwitched = new EventController();
            onPlayerPositionChanged = new EventController<Vector2>();
            OnTimeSwitchWithBoolParam = new EventController<bool>();
        }


    }
}