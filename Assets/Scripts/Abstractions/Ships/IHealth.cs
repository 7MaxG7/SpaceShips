using Infrastructure;

namespace Abstractions.Ships
{
    internal interface IHealth : IUpdater
    {
        float ShieldRecovery { get; }
        float MaxHp { get; }
        float MaxShield { get; }
        void RestoreHp();
        void RestoreShield();
    }
}