using System.Threading.Tasks;
using Enums;

namespace Sounds
{
    public interface ISoundPlayer
    {
        Task InitAsync();
        void PlayMusic();
        void PlayShoot(WeaponType weaponType);
        void StopAll();
    }
}