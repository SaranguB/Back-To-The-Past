using Events;

namespace Events
{
    public class EventService
    {
        public EventController onTimeSwitched;

        public EventService()
        {
            onTimeSwitched = new EventController();
        }


    }
}