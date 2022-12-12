using Abstractions.Ships;
using UnityEngine;

namespace Ships.Data
{
    internal class UpgradedWeaponsBattery : AbstractWeaponBattery, IDowngradable<IWeaponBattery>
    {
        private IWeaponBattery _baseWeaponBattery;
        private readonly IModule _module;

        public UpgradedWeaponsBattery(IWeaponBattery baseWeaponBattery, IModule module)
        {
            _baseWeaponBattery = baseWeaponBattery;
            _module = module;
        }

        protected override float GetDeltaCooldown(float deltaTime)
            => _module.IsCooldownRelativeReduce
                ? deltaTime / _module.Value
                : deltaTime;

        public IWeaponBattery Downgrade(IModule module)
        {
            if (_module == module)
                return _baseWeaponBattery;

            if (_baseWeaponBattery is not IDowngradable<IWeaponBattery> upgradedWeaponBattery)
            {
                Debug.LogError($"{this}: base weapons cannot be downgraded");
                return _baseWeaponBattery;
            }

            _baseWeaponBattery = upgradedWeaponBattery.Downgrade(module);
            return this;
        }
    }
}