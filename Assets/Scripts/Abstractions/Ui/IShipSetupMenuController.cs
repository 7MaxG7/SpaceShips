using System;
using System.Collections.Generic;
using Abstractions.Ships;
using Enums;
using Infrastructure;

namespace Abstractions.Ui
{
    internal interface IShipSetupMenuController : ICleaner
    {
        event Action OnSetupComplete;
        
        void PrepareUi(Dictionary<OpponentId, IShip> ships);
    }
}