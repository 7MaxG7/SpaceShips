using System.Collections.Generic;
using System.Linq;
using Configs.Data;
using Enums;
using Sounds;
using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(menuName = "Configs/" + nameof(SoundConfig), fileName = nameof(SoundConfig))]
    internal class SoundConfig : ScriptableObject
    {
        [SerializeField] private SoundPlayerView _soundPlayerPrefab;
        [SerializeField] private WeaponSound[] _weaponSounds;
        [SerializeField] private AudioClip _musicClip;

        public SoundPlayerView SoundPlayerPrefab => _soundPlayerPrefab;
        public AudioClip MusicClip => _musicClip;


        public Dictionary<WeaponType, AudioClip> GetWeaponShootingClips()
            => _weaponSounds.ToDictionary(clip => clip.WeaponType, clip => clip.AudioClip);
    }
}