using UnityEngine;

namespace Abstractions.Services
{
    internal interface IEquipmentFactory<TEquipment, TEquipType>
    {
        TEquipment CreateEquipment(TEquipType type, Transform parent);
    }
}