using System;
using System.Collections.Generic;
using Abstractions.Ships;
using Enums;
using Infrastructure;

namespace Abstractions.Ui
{
    internal interface IBattleUi : ICleaner
    {
        event Action OnBattleLeaved;
        
        void PrepareUi(Dictionary<OpponentId,IShip> ships);
        void ShowBattleEnd(IShip winner);
    }
}