using Abstractions.Ships;
using Configs.Data;
using UnityEngine;

namespace Abstractions.Services
{
    internal interface IShipsFactory
    {
        IShip CreateShip(ShipData ship, Vector3 position, Quaternion rotation);
        void GenerateView(IShip ship, Vector3 position, Quaternion rotation);
    }
}