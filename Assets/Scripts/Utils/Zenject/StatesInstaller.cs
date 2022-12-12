using Zenject;


namespace Infrastructure.Zenject
{
    internal sealed class StatesInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<IGameBootstrapState>().To<GameBootstrapState>().AsSingle();
            Container.Bind<IShipSetupState>().To<ShipSetupState>().AsSingle();
            Container.Bind<ILoadBattleState>().To<LoadBattleState>().AsSingle();
            Container.Bind<IRunBattleState>().To<RunBattleState>().AsSingle();
            Container.Bind<ILeaveBattleState>().To<LeaveBattleState>().AsSingle();

            Container.Bind<IGameStateMachine>().To<GameStateMachine>().AsSingle();
        }
    }
}