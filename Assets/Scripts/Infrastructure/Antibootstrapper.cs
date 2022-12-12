using UnityEngine;
using UnityEngine.SceneManagement;
using Utils;

namespace Infrastructure
{
    public class Antibootstrapper : MonoBehaviour
    {
        private void Awake()
        {
            if (FindObjectOfType<GameBootstrapper>() == null)
            {
                SceneManager.LoadScene(Constants.BOOTSTRAP_SCENE_NAME);
            }
            Destroy(gameObject);
        }
    }
}