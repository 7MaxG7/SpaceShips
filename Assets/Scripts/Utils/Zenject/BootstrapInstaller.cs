using Infrastructure;
using Zenject;

namespace Utils.Zenject
{
    public class BootstrapInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<IGame>().To<Game>().AsSingle();
            Container.Bind<IControllersHolder>().To<ControllersHolder>().AsSingle();
        }
    }
}