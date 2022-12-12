using System;
using Enums;
using Infrastructure;

namespace Abstractions.Ships
{
    public interface IModule : ICleaner
    {
        event Action<IModule> OnModuleUninstall;
        
        ModuleType ModuleType { get; }
        EffectType EffectType { get; }
        MathType MathType { get; }
        float Value { get; }
        
        bool IsCooldownRelativeReduce { get; }
        bool IsShieldRecoveryRelativeSpeedup { get; }
        bool IsHpConstantIncrease { get; }
        bool IsShieldConstantIncrease { get; }
    }
}