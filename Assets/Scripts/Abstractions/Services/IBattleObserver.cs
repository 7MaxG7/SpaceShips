using System;
using System.Collections.Generic;
using Abstractions.Ships;
using Infrastructure;

namespace Abstractions.Services
{
    public interface IBattleObserver : ISceneCleanable
    {
        event Action<IShip> OnWinnerDefined;
        
        HashSet<IShip> Ships { get; }

        void AddShip(IShip ship);
    }
}