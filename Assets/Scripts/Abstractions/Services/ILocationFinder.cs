using Enums;
using UnityEngine;

namespace Abstractions.Services
{
    public interface ILocationFinder
    {
        Vector3? GetOpponentLocation(OpponentId opponentId, out Quaternion rotation);
    }
}