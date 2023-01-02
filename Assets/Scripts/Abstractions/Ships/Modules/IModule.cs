using System;
using Enums;

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
    }
}