using System.Threading.Tasks;
using Infrastructure;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Abstractions.Services
{
    public interface IAssetsProvider : ISceneCleanable
    {
        void Init();
        Task WarmUpCurrentSceneAsync();
        Task<T> CreateInstanceAsync<T>(AssetReference assetReference, Transform parent = null, bool isDontDestroyAsset = false) where T : MonoBehaviour;
        Task<T> CreateInstanceAsync<T>(AssetReference assetReference, Vector3 position, Quaternion rotation
            , Transform parent = null, bool isPositioned = true, bool isDontDestroyAsset = false) where T : MonoBehaviour;
        Task<GameObject> CreateInstanceAsync(AssetReference assetReference, Transform parent = null);
    }
}