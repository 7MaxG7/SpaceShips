using System;
using Infrastructure;


namespace Services
{
    public interface ISceneLoader
    {
        void Init(ICoroutineRunner coroutineRunner);
        void LoadScene(string sceneName, Action onSceneLoadedCallback = null);
        string GetCurrentSceneName();
    }
}