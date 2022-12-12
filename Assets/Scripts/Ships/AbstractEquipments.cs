using System.Collections.Generic;
using Abstractions.Services;
using Infrastructure;
using UnityEngine;

namespace Ships
{
    internal abstract class AbstractEquipments<TEquipment, TEquipType> where TEquipment : ICleaner
    {
        public int MaxEquipmentsAmount { get; protected set; }
        
        protected IEquipmentFactory<TEquipment, TEquipType> EquipmentsFactory;
        protected readonly Dictionary<int, TEquipment> Equipments = new();
        private readonly Dictionary<int, Transform> _slots = new();

        
        public void SetSlots(Transform[] slots)
        {
            if (slots.Length < MaxEquipmentsAmount)
            {
                Debug.LogError($"{this}: Not enought weapon slots in ship view");
                return;
            }

            for (var i = 0; i < slots.Length; i++)
            {
                if (i >= MaxEquipmentsAmount)
                {
                    slots[i].gameObject.SetActive(false);
                    continue;
                }

                slots[i].gameObject.SetActive(true);
                _slots[i] = slots[i];
            }
        }

        public TEquipment GetEquipment(int index)
            => Equipments.TryGetValue(index, out var component) ? component : default;

        public virtual void SetEquipment(int index, TEquipType equipType)
        {
            if (index >= MaxEquipmentsAmount)
                return;

            if (!Equipments.TryGetValue(index, out var equipment))
                Equipments.Add(index, default);
            else
                equipment?.CleanUp();

            Equipments[index] = EquipmentsFactory.CreateEquipment(equipType, _slots[index]);
        }
    }
}