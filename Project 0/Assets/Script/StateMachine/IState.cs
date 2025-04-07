using UnityEngine;

namespace StateMachine
{
    public interface IState<T>
    {
        public T Owner { get; set; }
        public void OnStateEnter();
        public void UpdateState();
        public void OnStateExit();
        public void Update();
    }
}
