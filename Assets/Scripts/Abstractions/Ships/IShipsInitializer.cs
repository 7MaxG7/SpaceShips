using System.Collections.Generic;
using System.Threading.Tasks;
using Abstractions.Ships;
using Enums;
using Infrastructure;

namespace Abstractions
{
    public interface IShipsInitializer : ISceneCleanable
    {
        Dictionary<OpponentId, IShip> Ships { get; }
        
        Task PrepareShipsAsync();
    }
}