using System;
using Abstractions.Ships;

namespace Ships.Data
{
    internal abstract class AbstractHealth : IHealth
    {
        public event Action<float> OnHpChanged;
        public event Action<float> OnShieldChanged;

        public float ShieldRecovery { get; protected set; }
        public float MaxHp { get; protected set; }
        public float MaxShield { get; protected set; }

        public float CurrentHp
        {
            get => _currentHp;
            private set
            {
                _currentHp = value;
                _currentHp = Math.Clamp(value, 0, MaxHp);
                OnHpChanged?.Invoke(_currentHp);
            }
        }

        public float CurrentShield
        {
            get => _currentShield;
            private set
            {
                _currentShield = Math.Clamp(value, 0, MaxShield);
                OnShieldChanged?.Invoke(_currentShield);
            }
        }

        public float ShieldRecoveryInterval { get; protected set; }
        private float _shieldRecoverTimer;
        private float _currentHp;
        private float _currentShield;


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

        public void TakeDamage(int damage)
        {
            var damageLeft = damage - CurrentShield;
            CurrentShield -= damage;

            if (damageLeft > 0)
                CurrentHp -= Math.Min(CurrentHp, damageLeft);
        }

        private void RecoverShield(float deltaShield)
            => CurrentShield += deltaShield;
    }
}