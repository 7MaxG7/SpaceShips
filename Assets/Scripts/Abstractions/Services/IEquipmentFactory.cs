using UnityEngine;

namespace Abstractions.Services
{
    public interface IEquipmentFactory<TEquipment, TEquipType>
    {
        TEquipment CreateEquipment(TEquipType type, Transform parent);
        void GenerateView(TEquipment equipment, Transform parent);
    }
}