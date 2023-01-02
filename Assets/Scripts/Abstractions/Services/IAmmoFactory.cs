using System.Threading.Tasks;
using Abstractions.Ships;
using Infrastructure;

namespace Abstractions.Services
{
    public interface IAmmoFactory : ISceneCleanable
    {
        void PrepareRoot();
        Task<IAmmo> SpawnAmmo(IWeapon weapon);
    }
}