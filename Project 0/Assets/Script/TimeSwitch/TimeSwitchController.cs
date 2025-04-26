using Main;

namespace TimeSwitching
{
    public class TimeSwitchController
    {
        private TimeSwitchView timeSwitchView;
        private TimeStateMachine timeStateMachine;

        public TimeSwitchController(TimeSwitchView timeSwitchView)
        {
            InitializeVariable(timeSwitchView);
            SetController();
            CreateStateMachine();

            SubscribeToEvents();
        }

        public void SwitchTimeToPresent()
        {
            ChangeTimeState(TimeState.Present);
            timeSwitchView.SwitchTimeToPresent();
        }

        private void SubscribeToEvents()
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
        }

        private void CreateStateMachine()
            => timeStateMachine = new TimeStateMachine(this, timeSwitchView.GetAffectedObjects());

        public void SetPastProperties(TimeAffectedObjectController affectedObject)
        {
            if (affectedObject != null && affectedObject.gameObject.activeInHierarchy)
                affectedObject.SetPastProperties();
        }

        public void SetPresentproperties(TimeAffectedObjectController affectedObject)
        {
            if (affectedObject != null && affectedObject.gameObject.activeInHierarchy)
                affectedObject.SetPresentProperties();
        }

        private void SetController()
            => timeSwitchView.SetController(this);

        public void SwitchTime()
        {
            var currentState = timeStateMachine.GetCurrentState();

            if (currentState is PastState)
            {
                ChangeTimeState(TimeState.Present);
                timeSwitchView.SwitchTimeToPresent();
            }
            else if (currentState is PresentState)
            {
                ChangeTimeState(TimeState.Past);
                timeSwitchView.SwitchTimeToPast();
            }
            GameManager.Instance.eventService.OnTimeSwitchWithBoolParam.InvokeEvent(currentState is PastState);
        }

        public void ChangeTimeState(TimeState newState)
            =>timeStateMachine.ChangeState(newState);
    }
}
