using System.Threading.Tasks;
using Sounds;

namespace Abstractions.Services
{
    public interface IServicesFactory
    {
        Task<SoundPlayerView> CreateSoundPlayerAsync();
    }
}