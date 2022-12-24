using System;
using DG.Tweening;
using UnityEngine;

namespace Ui
{
    public class CurtainView : MonoBehaviour
    {
        [SerializeField] private CanvasGroup _canvasGroup;

        private float _animationDuration;


        public void Init(float animationDuration)
        {
            _animationDuration = animationDuration;
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
            _canvasGroup.DOFade(1, _animationDuration)
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

            _canvasGroup.DOFade(0, _animationDuration)
                .OnComplete(() =>
                {
                    gameObject.SetActive(false);
                    callback?.Invoke();
                });
        }

        public void HideCurtain(float startDelay, Action callback)
        {
            _canvasGroup.DOFade(0, _animationDuration).SetDelay(startDelay)
                .OnComplete(() =>
                {
                    gameObject.SetActive(false);
                    callback?.Invoke();
                });
        }
    }
}