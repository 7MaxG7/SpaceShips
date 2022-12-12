using Abstractions.Ships;
using UnityEngine;

namespace Ships.Data
{
    internal class UpgradedHealth : AbstractHealth, IDowngradable<IHealth>
    {
        public override float ShieldRecovery
            => _module.IsShieldRecoveryRelativeSpeedup
                ? _baseHealth.ShieldRecovery * _module.Value
                : _baseHealth.ShieldRecovery;

        public override float MaxHp
            => _module.IsHpConstantIncrease
                ? _baseHealth.MaxHp + _module.Value
                : _baseHealth.MaxHp;

        public override float MaxShield
            => _module.IsShieldConstantIncrease
                ? _baseHealth.MaxShield + _module.Value
                : _baseHealth.MaxShield;

        private IHealth _baseHealth;
        private readonly IModule _module;

        public UpgradedHealth(IHealth baseHealth, IModule module)
        {
            _baseHealth = baseHealth;
            _module = module;
        }

        public IHealth Downgrade(IModule module)
        {
            if (_module == module)
                return _baseHealth;

            if (_baseHealth is not IDowngradable<IHealth> upgradedHealth)
            {
                Debug.LogError($"{this}: base health cannot be downgraded");
                return _baseHealth;
            }

            _baseHealth = upgradedHealth.Downgrade(module);
            return this;
        }
    }
}