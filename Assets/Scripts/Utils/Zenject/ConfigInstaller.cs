using Configs;
using UnityEngine;
using Zenject;

namespace Utils.Zenject
{
    public class ConfigInstaller : MonoInstaller
    {
        [SerializeField] private RulesConfig _rulesConfig;

        public override void InstallBindings()
        {
            Container.Bind<RulesConfig>().FromScriptableObject(_rulesConfig).AsSingle();
        }
    }
}