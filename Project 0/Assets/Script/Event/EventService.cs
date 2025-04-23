using Events;
using UnityEngine;


namespace Events
{
    public class EventService
    {
        public EventController onTimeSwitched;
        public EventController<Vector2> onPlayerPositionChanged;
        public EventController<bool> OnTimeSwitchWithBoolParam;
        public EventController OnPlayerGotKey;
        public EventController OnPlayerOpenedDoor;
        public EventController OnPlayerFinishedLevel;
        public EventController OnPlayerDead;
        public EventController<int> OnPlayerGotDamaged;
        public EventService()
        {
            onTimeSwitched = new EventController();
            onPlayerPositionChanged = new EventController<Vector2>();
            OnTimeSwitchWithBoolParam = new EventController<bool>();
            OnPlayerGotKey = new EventController();
            OnPlayerOpenedDoor = new EventController();
            OnPlayerGotDamaged = new EventController<int>();
            OnPlayerFinishedLevel = new EventController();
            OnPlayerDead = new EventController();
        }

    }
}