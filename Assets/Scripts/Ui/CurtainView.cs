using System;
using Configs;
using DG.Tweening;
using UnityEngine;

namespace Ui
{
    public class CurtainView : MonoBehaviour
    {
        [SerializeField] private CanvasGroup _canvasGroup;
        private UiConfig _uiConfig;

        
        public void Init(UiConfig uiConfig)
        {
            _uiConfig = uiConfig;
            DontDestroyOnLoad(this);
        }

        public void ShowCurtain(bool isAnimated, Action callback)
        {
            _canvasGroup.DOKill();
            gameObject.SetActive(true);
            if (!isAnimated)
            {
                _canvasGroup.alpha = 1;
                callback?.Invoke();
                return;
            }

            _canvasGroup.alpha = 0;
            _canvasGroup.DOFade(1, _uiConfig.CurtainAnimDuration)
                .OnComplete(() => callback?.Invoke());
        }

        public void HideCurtain(bool isAnimated, Action callback)
        {
            _canvasGroup.DOKill();
            if (!isAnimated)
            {
                gameObject.SetActive(false);
                callback?.Invoke();
                return;
            }

            _canvasGroup.DOFade(0, _uiConfig.CurtainAnimDuration)
                .OnComplete(() =>
                {
                    gameObject.SetActive(false);
                    callback?.Invoke();
                });
        }
    }
}