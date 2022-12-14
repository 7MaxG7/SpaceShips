namespace Infrastructure
{
    public interface IUpdater : IController
    {
        public void OnUpdate(float deltaTime);
    }
}