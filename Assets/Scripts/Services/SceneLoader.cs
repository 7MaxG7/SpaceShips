using System;
using System.Collections;
using Infrastructure;
using UnityEngine;
using UnityEngine.SceneManagement;


namespace Services
{
    internal sealed class SceneLoader : ISceneLoader
    {
        private ICoroutineRunner _coroutineRunner;


        public void Init(ICoroutineRunner coroutineRunner)
        {
            _coroutineRunner = coroutineRunner;
        }

        public void LoadScene(string sceneName, Action onSceneLoadedCallback = null)
            => _coroutineRunner.StartCoroutine(LoadSceneCoroutine(sceneName, onSceneLoadedCallback));

        public string GetCurrentSceneName() 
            => SceneManager.GetActiveScene().name;

        private IEnumerator LoadSceneCoroutine(string sceneName, Action onSceneLoadedCallback)
        {
            if (GetCurrentSceneName() == sceneName)
            {
                onSceneLoadedCallback?.Invoke();
                yield break;
            }

            var loadSceneOperation = SceneManager.LoadSceneAsync(sceneName);
            while (!loadSceneOperation.isDone)
                yield return new WaitForEndOfFrame();

            onSceneLoadedCallback?.Invoke();
        }
    }
}