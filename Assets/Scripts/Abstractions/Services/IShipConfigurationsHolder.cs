using System.Collections.Generic;
using Configs.Data;
using Enums;
using Ships;

namespace Abstractions.Services
{
    public interface IShipConfigurationsHolder
    {
        Dictionary<OpponentId, ShipModel> ShipModels { get; }
        
        void Init(Opponent[] opponents);
    }
}