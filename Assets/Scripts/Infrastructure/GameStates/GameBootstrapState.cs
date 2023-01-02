﻿using System.Threading.Tasks;
using Abstractions.Services;
using Configs;
using DG.Tweening;
using Services;
using Sounds;
using Utils;
using Zenject;


namespace Infrastructure
{
    internal sealed class GameBootstrapState : IGameState
    {
        private readonly IStaticDataService _staticDataService;
        private readonly ISceneLoader _sceneLoader;
        private readonly ICurtain _curtain;
        private readonly IAssetsProvider _assetsProvider;
        private readonly ISoundPlayer _soundPlayer;
        private readonly RulesConfig _rulesConfig;
        private readonly IShipConfigurationsHolder _configurationsHolder;
        private IGameStateMachine _stateMachine;


        [Inject]
        public GameBootstrapState(IStaticDataService staticDataService, ISceneLoader sceneLoader, ICurtain curtain
            , IAssetsProvider assetsProvider, ISoundPlayer soundPlayer, RulesConfig rulesConfig
            , IShipConfigurationsHolder configurationsHolder)
        {
            _staticDataService = staticDataService;
            _sceneLoader = sceneLoader;
            _curtain = curtain;
            _assetsProvider = assetsProvider;
            _soundPlayer = soundPlayer;
            _rulesConfig = rulesConfig;
            _configurationsHolder = configurationsHolder;
        }

        public async void Enter()
        {
            await PrepareServicesAsync();
            _sceneLoader.LoadScene(Constants.SETUP_SCENE_NAME, _stateMachine.Enter<ShipSetupState>);
        }

        public void Exit()
        {
        }

        public void Init(IGameStateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }

        private async Task PrepareServicesAsync()
        {
            DOTween.Init();
            _assetsProvider.Init();
            await _curtain.InitAsync();
            _curtain.ShowCurtain(false);
            _staticDataService.Init();
            _configurationsHolder.Init(_rulesConfig.Opponents);
            await _soundPlayer.InitAsync();
        }
    }
}