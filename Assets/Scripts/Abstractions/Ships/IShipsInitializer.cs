using System.Collections.Generic;
using Abstractions.Ships;
using Enums;
using Infrastructure;

namespace Abstractions
{
    internal interface IShipsInitializer : ICleaner
    {
        Dictionary<OpponentId, IShip> Ships { get; }
        
        void PrepareShips();
    }
}