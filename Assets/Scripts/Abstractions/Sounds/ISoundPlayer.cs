using Enums;

namespace Sounds
{
    internal interface ISoundPlayer
    {
        void Init();
        void PlayMusic();
        void PlayShoot(WeaponType weaponType);
        void StopAll();
    }
}