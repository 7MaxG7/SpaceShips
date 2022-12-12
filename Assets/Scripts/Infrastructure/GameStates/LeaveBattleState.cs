using System;


namespace Infrastructure
{
    internal sealed class LeaveBattleState : ILeaveBattleState
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