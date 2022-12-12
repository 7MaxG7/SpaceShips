using UnityEngine;


namespace Sounds
{
    internal sealed class SoundPlayerView : MonoBehaviour
    {
        [SerializeField] private AudioSource _musicSource;
        [SerializeField] private AudioSource _sfxSource;

        public float SoundVolume
        {
            get => _sfxSource.volume;
            set => _sfxSource.volume = value;
        }

        public float MusicVolume
        {
            get => _musicSource.volume;
            set => _musicSource.volume = value;
        }

        public bool MusicLoop
        {
            set => _musicSource.loop = value;
        }


        public void PlaySound(AudioClip audioClip)
        {
            _sfxSource.PlayOneShot(audioClip);
        }

        public void StopMusic()
        {
            _musicSource.Stop();
        }

        public void PlayMusic(AudioClip audioClip)
        {
            _musicSource.clip = audioClip;
            _musicSource.Play();
        }

        public void StopSound()
        {
            _sfxSource.Stop();
        }
    }
}