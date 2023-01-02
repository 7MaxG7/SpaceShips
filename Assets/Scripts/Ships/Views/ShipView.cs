﻿using Abstractions.Ships;
using Ships.Views;
using UnityEngine;

namespace Ships
{
    public sealed class ShipView : MonoBehaviour, IDamagableView
    {
        [SerializeField] private ShieldView _shield;
        [SerializeField] private Transform[] _weaponSlots;
        [SerializeField] private Transform[] _moduleSlots;

        public Transform[] WeaponSlots => _weaponSlots;
        public Transform[] ModuleSlots => _moduleSlots;
        public ShieldView Shield => _shield;
    }
}