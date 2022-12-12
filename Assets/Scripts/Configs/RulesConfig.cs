using Configs.Data;
using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(menuName = "Configs/" + nameof(RulesConfig), fileName = nameof(RulesConfig))]
    public class RulesConfig : ScriptableObject
    {
        [SerializeField] private Opponent[] _opponents;

        public Opponent[] Opponents => _opponents;
    }
}