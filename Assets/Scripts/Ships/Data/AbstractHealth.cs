using System;
using Abstractions.Ships;

namespace Ships.Data
{
    internal abstract class AbstractHealth : IHealth
    {
        public abstract float ShieldRecovery { get; }
        public abstract float MaxHp { get; }
        public abstract float MaxShield { get; }
        
        protected float CurrentHp;
        protected float CurrentShield;
        protected float ShieldRecoveryInterval { get; set; }
        private float _shieldRecoverTimer;


        public void OnUpdate(float deltaTime)
        {
            _shieldRecoverTimer += deltaTime;
            if (_shieldRecoverTimer < ShieldRecoveryInterval)
                return;

            _shieldRecoverTimer -= ShieldRecoveryInterval;
            RecoverShield(ShieldRecovery);
        }

        public void RestoreHp()
            => CurrentHp = MaxHp;

        public void RestoreShield()
            => CurrentShield = MaxShield;

        private void RecoverShield(float deltaShield)
        {
            CurrentShield += deltaShield;
            CurrentShield = Math.Clamp(CurrentShield, 0, MaxShield);
        }
    }
}