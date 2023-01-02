using System.Collections.Generic;
using System.Threading.Tasks;
using Abstractions.Services;
using Configs;
using Infrastructure;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using Zenject;
using static UnityEngine.Object;


namespace Services
{
    internal sealed class AssetsProvider : IAssetsProvider
    {
        private readonly ISceneLoader _sceneLoader;
        private readonly AssetsConfig _assetsConfig;

        private readonly Dictionary<string, AsyncOperationHandle> _loadedAssets = new();
        private readonly List<AsyncOperationHandle> _handles = new();
        private bool _isCleaned;


        [Inject]
        public AssetsProvider(ISceneLoader sceneLoader, ICleaner cleaner, AssetsConfig assetsConfig)
        {
            _sceneLoader = sceneLoader;
            _assetsConfig = assetsConfig;
            cleaner.AddCleanable(this);
        }
                
        public void CleanUp()
        {
            if (_isCleaned)
                return;

            _isCleaned = true;
            foreach (var handle in _handles) 
                Addressables.Release(handle);
            _handles.Clear();
            _loadedAssets.Clear();
        }

        public void Init()
        {
            Addressables.InitializeAsync();
        }

        public async Task WarmUpCurrentSceneAsync()
        {
            var sceneName = _sceneLoader.GetCurrentSceneName();
            var assetReferences = _assetsConfig.GetAssetReferencesForState(sceneName);
            foreach (var reference in assetReferences) 
                await LoadAsync(reference);
        }

        public async Task<T> CreateInstanceAsync<T>(AssetReference assetReference, Transform parent = null) where T : MonoBehaviour
            => await CreateInstanceAsync<T>(assetReference, Vector3.zero, Quaternion.identity, parent, false);

        public async Task<T> CreateInstanceAsync<T>(AssetReference assetReference, Vector3 position, Quaternion rotation
            , Transform parent = null, bool isPositioned = true) where T : MonoBehaviour
        {
            var prefab = await LoadAsync(assetReference);
            return isPositioned
                ? Instantiate(prefab, position, rotation, parent).GetComponent<T>()
                : Instantiate(prefab, parent).GetComponent<T>();
        }

        public async Task<GameObject> CreateInstanceAsync(AssetReference assetReference, Transform parent = null)
        {
            var prefab = await LoadAsync(assetReference);
            return Instantiate(prefab, parent);
        }

        private async Task<GameObject> LoadAsync(AssetReference assetReference)
        {
            _isCleaned = false;
            
            if (_loadedAssets.TryGetValue(assetReference.AssetGUID, out var loadedHandle))
                return loadedHandle.Result as GameObject;
            
            var handle = Addressables.LoadAssetAsync<GameObject>(assetReference);
            handle.Completed += resultHandle =>
            {
                _loadedAssets[assetReference.AssetGUID] = resultHandle;
                _handles.Add(handle);
            };
            return await handle.Task;
        }
    }
}