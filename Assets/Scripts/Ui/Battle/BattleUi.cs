using System;
using System.Collections.Generic;
using Abstractions.Services;
using Abstractions.Ships;
using Abstractions.Ui;
using Enums;
using UnityEngine;
using Zenject;

namespace Ui.Battle
{
    internal class BattleUi : IBattleUi
    {
        public event Action OnBattleLeaved;
        
        private readonly IAssetsProvider _assetsProvider;
        private Dictionary<OpponentId,IShip> _ships = new();
        private BattleUiView _ui;


        [Inject]
        public BattleUi(IAssetsProvider assetsProvider)
        {
            _assetsProvider = assetsProvider;
        }
        
        public void PrepareUi(Dictionary<OpponentId, IShip> ships)
        {
            _ships = ships;
            _ui = _assetsProvider.CreateBattleUi();
            foreach (var (opponent, ship) in _ships)
            {
                var healthPanel = _ui.GetHealthPanel(opponent);
                if (healthPanel == null)
                {
                    Debug.LogError($"{this}: No health panel for opponent {opponent}");
                    continue;
                }

                ship.Health.OnHpChanged += healthPanel.SetCurrentHp;
                healthPanel.SetCurrentHp(ship.Health.CurrentHp, ship.Health.MaxHp);
                ship.Health.OnShieldChanged += healthPanel.SetCurrentShield;
                healthPanel.SetCurrentShield(ship.Health.CurrentShield, ship.Health.MaxShield);
            }
            _ui.LeaveButton.onClick.AddListener(() => OnBattleLeaved?.Invoke());
            _ui.HideBattleEndObjects();
        }

        public void ShowBattleEnd(IShip winner) 
            => _ui.ShowWinnerLable(winner);


        public void CleanUp()
        {
            _ui.LeaveButton.onClick.RemoveAllListeners();
            foreach (var (opponent, ship) in _ships)
            {
                var healthPanel = _ui.GetHealthPanel(opponent);
                ship.Health.OnHpChanged -= healthPanel.SetCurrentHp;
                ship.Health.OnShieldChanged -= healthPanel.SetCurrentShield;
            }
        }
    }
}