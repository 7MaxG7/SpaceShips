using Configs;
using UnityEngine;
using Zenject;

namespace Utils.Zenject
{
    public class ConfigInstaller : MonoInstaller
    {
        [SerializeField] private RulesConfig _rulesConfig;
        [SerializeField] private UiConfig _uiConfig;
        [SerializeField] private SoundConfig _soundConfig;

        public override void InstallBindings()
        {
            Container.Bind<RulesConfig>().FromScriptableObject(_rulesConfig).AsSingle();
            Container.Bind<UiConfig>().FromScriptableObject(_uiConfig).AsSingle();
            Container.Bind<SoundConfig>().FromScriptableObject(_soundConfig).AsSingle();
        }
    }
}