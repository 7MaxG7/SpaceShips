using System.Collections.Generic;


namespace Infrastructure
{
    internal sealed class ControllersHolder : IControllersHolder
    {
        private readonly List<IUpdater> _updaters = new();
        private readonly List<ICleaner> _cleaners = new();

        public void OnUpdate(float deltaTime)
        {
            foreach (var updater in _updaters)
            {
                updater.OnUpdate(deltaTime);
            }
        }

        public void AddController(IController controller)
        {
            if (controller is IUpdater updater)
                _updaters.Add(updater);
            if (controller is ICleaner cleaner)
                _cleaners.Add(cleaner);
        }

        public void RemoveController(IController controller)
        {
            if (controller is IUpdater updater)
                _updaters.Remove(updater);
            if (controller is ICleaner cleaner)
                _cleaners.Remove(cleaner);
        }

        public void ClearControllers()
        {
            foreach (var cleaner in _cleaners)
            {
                cleaner.CleanUp();
            }
            _cleaners.Clear();
            _updaters.Clear();
        }
    }
}