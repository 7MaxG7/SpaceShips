using Abstractions.Services;
using Abstractions.Ships;
using Enums;
using Ships;
using UnityEngine;
using Zenject;

namespace Services
{
    internal class ModuleFactory : IModuleFactory
    {
        private readonly IStaticDataService _staticDataService;
        private readonly IAssetsProvider _assetsProvider;


        [Inject]
        public ModuleFactory(IStaticDataService staticDataService, IAssetsProvider assetsProvider)
        {
            _staticDataService = staticDataService;
            _assetsProvider = assetsProvider;
        }
        
        public IModule CreateEquipment(ModuleType moduleType, Transform parent)
        {
            var moduleData = _staticDataService.GetModuleData(moduleType);
            var moduleView = _assetsProvider.CreateModule(moduleType, parent);
            return new Module(moduleView, moduleData.EffectType, moduleData.MathType, moduleData.Value, moduleData.ModuleType);
        }
    }
}