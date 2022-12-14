using System.Collections.Generic;
using Abstractions.Services;
using Configs;
using Enums;
using UnityEngine;
using Zenject;
using Object = UnityEngine.Object;


namespace Sounds
{
    internal sealed class SoundPlayer : ISoundPlayer
    {
        private readonly SoundConfig _soundConfig;
        private readonly IAssetsProvider _assetsProvider;
        private SoundPlayerView _soundPlayer;
        private Dictionary<WeaponType, AudioClip> _weaponShootClips;
        private AudioClip _musicClip;
        private bool _musicIsPlaying;


        [Inject]
        public SoundPlayer(SoundConfig soundConfig, IAssetsProvider assetsProvider)
        {
            _soundConfig = soundConfig;
            _assetsProvider = assetsProvider;
        }

        public void Init()
        {
            InitPlayer();
            InitClips();
        }

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

        public void PlayShoot(WeaponType weaponType)
        {
            if (_weaponShootClips.ContainsKey(weaponType))
                _soundPlayer.PlaySound(_weaponShootClips[weaponType]);
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
            _weaponShootClips = _soundConfig.GetWeaponShootingClips();
            _musicClip = _soundConfig.MusicClip;
        }
    }
}