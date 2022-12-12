using System;


namespace Infrastructure
{
    internal sealed class RunBattleState : IRunBattleState
    {
        public event Action OnStateChange;

        public void Enter()
        {
        }

        public void Exit()
        {
        }

        private void SwitchState()
        {
            OnStateChange?.Invoke();
        }
    }
}