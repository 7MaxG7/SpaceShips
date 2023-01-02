using System;
using System.Collections.Generic;
using Abstractions.Services;
using Enums;
using Infrastructure;
using Ships;
using UnityEngine;

namespace Ui.ShipSetup
{
    public class AbstractEquipmentSelectController<TType> : ICleanable where TType : Enum
    {
        protected readonly AbstractEquipmentSelectView<TType> EquipmentSelectView;
        protected readonly Dictionary<OpponentId, ShipModel> ShipModels;

        protected OpponentId OpponentId;
        protected int SlotIndex;
        
        private IUiFactory _uiFactory;
        private ICoroutineRunner _coroutineRunner;
        private float _fadeAnimDuration;



        protected AbstractEquipmentSelectController(AbstractEquipmentSelectView<TType> equipmentSelectView
            , Dictionary<OpponentId,ShipModel> shipModels)
        {
            EquipmentSelectView = equipmentSelectView;
            ShipModels = shipModels;
        }

        public void CleanUp()
        {
            EquipmentSelectView.CleanUp();
        }

        public void Show(OpponentId opponentId, int slotIndex, Vector3 position)
        {
            OpponentId = opponentId;
            SlotIndex = slotIndex;
            EquipmentSelectView.Locate(opponentId, position);
            EquipmentSelectView.Show();
        }

        public void Hide() 
            => EquipmentSelectView.Hide();
    }
}