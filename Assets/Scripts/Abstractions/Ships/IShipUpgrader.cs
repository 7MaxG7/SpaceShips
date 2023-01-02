using Abstractions.Ships;
using Ships;

namespace Services
{
    public interface IShipUpgrader
    {
        void Upgrade(Ship ship, IModule module);
        void Downgrade(Ship ship, IModule module);
    }
}