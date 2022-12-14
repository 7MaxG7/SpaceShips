using System;
using Infrastructure;

namespace Abstractions.Ships
{
    public interface IHealth : IUpdater
    {
        event Action<float> OnHpChanged;
        event Action<float> OnShieldChanged;
        
        float ShieldRecovery { get; }
        float MaxHp { get; }
        float MaxShield { get; }
        float CurrentHp { get; }
        float CurrentShield { get; }
        float ShieldRecoveryInterval { get; }

        void RestoreHp();
        void RestoreShield();
        void TakeDamage(int damage);
    }
}