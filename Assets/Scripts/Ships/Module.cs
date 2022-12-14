using System;
using Abstractions.Ships;
using Enums;
using Ships.Views;
using Object = UnityEngine.Object;

namespace Ships
{
    public class Module : IModule
    {
        public event Action<IModule> OnModuleUnequip;

        public ModuleType ModuleType { get; }
        public EffectType EffectType { get; }
        public MathType MathType { get; }
        public float Value { get; }
        
        public bool IsReloadRelativeReduce => EffectType == EffectType.ShootCooldown && MathType == MathType.Relative;
        public bool IsShieldRecoveryRelativeSpeedup
            => EffectType == EffectType.ShieldRecovery && MathType == MathType.Relative;
        public bool IsHpConstantIncrease => EffectType == EffectType.Hp && MathType == MathType.Constant;
        public bool IsShieldConstantIncrease => EffectType == EffectType.Shield && MathType == MathType.Constant;

        private ModuleView _moduleView;


        public Module(EffectType effectType, MathType mathType, float value, ModuleType moduleType)
        {
            EffectType = effectType;
            MathType = mathType;
            Value = value;
            ModuleType = moduleType;
        }

        public void SetView(ModuleView view)
            => _moduleView = view;

        public void CleanUp()
        {
        }

        public void Unequip()
        {
            OnModuleUnequip?.Invoke(this);
            Object.Destroy(_moduleView.gameObject);
        }
    }
}