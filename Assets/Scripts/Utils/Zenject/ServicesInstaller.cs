using Abstractions;
using Abstractions.Services;
using Infrastructure;
using Services;
using Ships;
using Sounds;
using Zenject;

namespace Utils.Zenject
{
    public class ServicesInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<Game>().AsSingle();
            Container.Bind<ICurtain>().To<Curtain>().AsSingle();
            Container.Bind<IUpdater>().To<Updater>().AsSingle();
            Container.Bind<ICleaner>().To<Cleaner>().AsSingle();
            Container.Bind<IShipsInitializer>().To<ShipsInitializer>().AsSingle();
            Container.Bind<IStaticDataService>().To<StaticDataService>().AsSingle();
            Container.Bind<IAssetsProvider>().To<AssetsProvider>().AsSingle();
            Container.Bind<ISceneLoader>().To<SceneLoader>().AsSingle();
            Container.Bind<IShipsFactory>().To<ShipsFactory>().AsSingle();
            Container.Bind<IAmmoPool>().To<AmmoPool>().AsSingle();
            Container.Bind<IAmmoFactory>().To<AmmoFactory>().AsSingle();
            Container.Bind<IWeaponFactory>().To<WeaponFactory>().AsSingle();
            Container.Bind<IModuleFactory>().To<ModuleFactory>().AsSingle();
            Container.Bind<IShipUpgrader>().To<ShipUpgrader>().AsSingle();
            Container.Bind<IShipsInteractor>().To<ShipsInteractor>().AsSingle();
            Container.Bind<ILocationFinder>().To<LocationFinder>().AsSingle();
            Container.Bind<ISoundPlayer>().To<SoundPlayer>().AsSingle();
        }
    }
}