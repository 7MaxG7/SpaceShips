using Abstractions.Services;
using Services;
using Zenject;

namespace Utils.Zenject
{
    public class ServicesInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<IStaticDataService>().To<StaticDataService>().AsSingle();
            Container.Bind<IAssetsProvider>().To<AssetsProvider>().AsSingle();
            Container.Bind<ISceneLoader>().To<SceneLoader>().AsSingle();
            Container.Bind<IShipsFactory>().To<ShipsFactory>().AsSingle();
            Container.Bind<IAmmoPool>().To<AmmoPool>().AsSingle();
            Container.Bind<IAmmoFactory>().To<AmmoFactory>().AsSingle();
            Container.Bind<IWeaponFactory>().To<WeaponFactory>().AsSingle();
            Container.Bind<IModuleFactory>().To<ModuleFactory>().AsSingle();
            Container.Bind<IShipUpgrader>().To<ShipUpgrader>().AsSingle();
        }
    }
}