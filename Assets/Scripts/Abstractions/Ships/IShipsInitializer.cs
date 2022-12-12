using System.Collections.Generic;
using Abstractions.Ships;
using Enums;

namespace Abstractions
{
    internal interface IShipsInitializer
    {
        Dictionary<OpponentId, IShip> Ships { get; }
        
        void PrepareShips();
    }
}