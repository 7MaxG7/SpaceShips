namespace Sounds
{
    internal interface ISoundController
    {
        void Init();
        // void PlayWeaponShootSound(WeaponType weaponType);
        void PlayMusic();
        void StopAll();
    }
}