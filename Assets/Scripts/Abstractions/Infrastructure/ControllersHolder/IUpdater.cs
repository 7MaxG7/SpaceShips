namespace Infrastructure
{
    internal interface IUpdater : IController
    {
        public void OnUpdate(float deltaTime);
    }
}