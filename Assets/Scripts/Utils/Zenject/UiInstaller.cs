using Abstractions.Ui;
using Ui.ShipSetup.Controllers;
using Zenject;

namespace Utils.Zenject
{
    public class UiInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<IShipSetupMenuController>().To<ShipSetupMenuController>().AsSingle();
        }
    }
}