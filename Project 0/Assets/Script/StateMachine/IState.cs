using UnityEngine;

namespace StateMachine
{
    public interface IState<T>
    {
        public T owner { get; set; }
        public void OnStateEnter();
        public void UpdateState();
        public void FixedUpdateState();
        public void OnStateExit();
    }
}
