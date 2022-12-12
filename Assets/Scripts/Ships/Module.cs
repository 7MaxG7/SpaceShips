using System;
using Abstractions.Ships;
using Enums;
using Ships.Views;
using Object = UnityEngine.Object;

namespace Ships
{
    public class Module : IModule
    {
        public event Action<IModule> OnModuleUninstall;

        public ModuleType ModuleType { get; }
        public EffectType EffectType { get; }
        public MathType MathType { get; }
        public float Value { get; }
        
        public bool IsCooldownRelativeReduce => EffectType == EffectType.ShootCooldown && MathType == MathType.Relative;
        public bool IsShieldRecoveryRelativeSpeedup
            => EffectType == EffectType.ShieldRecovery && MathType == MathType.Relative;
        public bool IsHpConstantIncrease => EffectType == EffectType.Hp && MathType == MathType.Constant;
        public bool IsShieldConstantIncrease => EffectType == EffectType.Shield && MathType == MathType.Constant;

        private readonly ModuleView _moduleView;


        public Module(ModuleView moduleView, EffectType effectType, MathType mathType, float value, ModuleType moduleType)
        {
            _moduleView = moduleView;
            EffectType = effectType;
            MathType = mathType;
            Value = value;
            ModuleType = moduleType;
        }

        public void CleanUp()
        {
            OnModuleUninstall?.Invoke(this);
            Object.Destroy(_moduleView.gameObject);
        }
    }
}