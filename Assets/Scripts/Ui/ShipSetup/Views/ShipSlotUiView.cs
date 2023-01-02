﻿using System.Linq;
using Enums;
using Ui.ShipSetup.Data;
using UnityEngine;

namespace Ui.ShipSetup
{
    public sealed class ShipSlotUiView : SlotUiView
    {
        [SerializeField] private SelectPanelAnchor[] _selectPanelAnchor;
        
        public int Index { get; private set; }
        public Transform SelectPanelAnchor { get; private set; }

        
        public void Init(OpponentId opponentId, int index)
        {
            Index = index;
            SelectPanelAnchor = _selectPanelAnchor.FirstOrDefault(anchor => anchor.OpponentId == opponentId)?.Anchor;
            SetIcon(null);
        }
    }
}