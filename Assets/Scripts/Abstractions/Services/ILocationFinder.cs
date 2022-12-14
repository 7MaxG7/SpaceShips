using Enums;
using UnityEngine;

namespace Abstractions.Services
{
    internal interface ILocationFinder
    {
        Vector3? GetOpponentLocation(OpponentId opponentId, out Quaternion rotation);
    }
}