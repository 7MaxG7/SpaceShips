using System.Threading.Tasks;
using UnityEngine;

namespace Abstractions.Services
{
    public interface IEquipmentFactory<TEquipment, in TEquipType>
    {
        Task<TEquipment> CreateEquipment(TEquipType type, Transform parent);
    }
}