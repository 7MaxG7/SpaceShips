using System;


namespace Infrastructure
{
    internal interface IGameState
    {
        public event Action OnStateChange;

        public void Enter();
        public void Exit();
    }
}