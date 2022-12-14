namespace Infrastructure
{
    internal interface IControllersHolder : IUpdater, ICleaner
    {
        void AddController(IController controller);
        void RemoveController(IController controller);
    }
}