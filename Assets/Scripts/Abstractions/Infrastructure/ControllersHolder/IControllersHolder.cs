namespace Infrastructure
{
    internal interface IControllersHolder : IUpdater
    {
        void AddController(IController controller);
        void RemoveController(IController controller);
        void ClearControllers();
    }
}