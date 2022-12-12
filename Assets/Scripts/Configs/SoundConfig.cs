using Sounds;
using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(menuName = "Configs/" + nameof(SoundConfig), fileName = nameof(SoundConfig))]
    internal class SoundConfig : ScriptableObject
    {
        public SoundPlayerView SoundPlayerPrefab { get; }
        public AudioClip MenuMusic { get; }
        

        public object GetWeaponShootingClips()
        {
            return null;
        }
    }
}