﻿using System.Threading.Tasks;
using Abstractions.Services;
using Configs;
using Sounds;
using Zenject;

namespace Services
{
    public sealed class ServicesFactory : IServicesFactory
    {
        private readonly IAssetsProvider _assetsProvider;
        private readonly SoundConfig _soundConfig;


        [Inject]
        public ServicesFactory(IAssetsProvider assetsProvider, SoundConfig soundConfig)
        {
            _assetsProvider = assetsProvider;
            _soundConfig = soundConfig;
        }

        public async Task<SoundPlayerView> CreateSoundPlayerAsync() 
            => await _assetsProvider.CreateInstanceAsync<SoundPlayerView>(_soundConfig.SoundPlayerPrefab, isDontDestroyAsset: true);
    }
}