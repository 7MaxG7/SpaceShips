using Abstractions.Services;
using Configs;
using UnityEngine;
using Zenject;
using Object = UnityEngine.Object;


namespace Sounds
{
    internal sealed class SoundController : ISoundController
    {
        private readonly SoundConfig _soundConfig;
        private readonly IAssetsProvider _assetsProvider;
        private SoundPlayerView _soundPlayer;
        // private Dictionary<WeaponType, AudioClip> _weaponShootClips;
        private AudioClip _musicClip;
        private bool _musicIsPlaying;


        [Inject]
        public SoundController(SoundConfig soundConfig, IAssetsProvider assetsProvider)
        {
            _soundConfig = soundConfig;
            _assetsProvider = assetsProvider;
        }

        public void Init()
        {
            InitPlayer();
            InitClips();
        }

        // public void PlayWeaponShootSound(WeaponType weaponType)
        // {
        //     if (_weaponShootClips.ContainsKey(weaponType))
        //         _soundPlayer.PlaySound(_weaponShootClips[weaponType]);
        // }

        public void PlayMusic()
        {
            if (_musicIsPlaying || _musicClip == null)
                return;

            _soundPlayer.StopMusic();
            _soundPlayer.PlayMusic(_musicClip);
            _musicIsPlaying = true;
        }


        public void StopAll()
        {
            _soundPlayer.StopMusic();
            _soundPlayer.StopSound();
            _musicIsPlaying = false;
        }

        private void InitPlayer()
        {
            if (_soundPlayer == null)
                _soundPlayer = _assetsProvider.CreateSoundPlayer();
            _soundPlayer.MusicLoop = true;
            Object.DontDestroyOnLoad(_soundPlayer);
        }

        private void InitClips()
        {
            // _weaponShootClips = _soundConfig.GetWeaponShootingClips();
            _musicClip = _soundConfig.MenuMusic;
        }
    }
}