using Enums;
using UnityEngine;

namespace Ships.Views
{
    public class ShipSpawnerMarker : MonoBehaviour
    {
        [SerializeField] private OpponentId _opponentId;

        public OpponentId OpponentId => _opponentId;
    }
}