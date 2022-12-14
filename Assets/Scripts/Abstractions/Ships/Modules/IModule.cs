using System;
using Enums;
using Ships.Views;

namespace Abstractions.Ships
{
    public interface IModule : IEquipment
    {
        event Action<IModule> OnModuleUnequip;
        
        ModuleType ModuleType { get; }
        EffectType EffectType { get; }
        float Value { get; }
        
        bool IsReloadRelativeReduce { get; }
        bool IsShieldRecoveryRelativeSpeedup { get; }
        bool IsHpConstantIncrease { get; }
        bool IsShieldConstantIncrease { get; }
        
        void SetView(ModuleView view);
    }
}