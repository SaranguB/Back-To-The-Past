using System;
using System.Collections.Generic;

namespace StateMachine
{
    public class GenericStateMachine<T, TEnum> where TEnum : Enum
    {
        protected T owner;
        protected IState<T> currentState;
        protected Dictionary<TEnum, IState<T>> states = new();

        public GenericStateMachine(T owner)
        {
            this.owner = owner;
        }

        public void ChangeState(TEnum key)
        {
            if (states.TryGetValue(key, out var newState))
            {
                currentState?.OnStateExit();
                currentState = newState;
                currentState.owner = owner;
                currentState?.OnStateEnter();
            }
        }

        protected void SetOwner()
        {
            foreach (var state in states.Values)
            {
                state.owner = owner;
            }
        }

        protected void AddState(TEnum key, IState<T> state)
        {
            states[key] = state;
        }

        public void Update() 
            => currentState?.UpdateState();

        public void FixedUpdate() 
            => currentState?.FixedUpdateState();

        public IState<T> GetCurrentState()
            => currentState;
    }
}
