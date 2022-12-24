using System;
using Abstractions.Services;
using Configs;
using Ui;
using Zenject;

namespace Services
{
    internal class Curtain : ICurtain
    {
        private readonly UiConfig _uiConfig;
        private CurtainView _curtainView;

        
        [Inject]
        public Curtain(UiConfig uiConfig)
        {
            _uiConfig = uiConfig;
        }

        public void Prepare(CurtainView curtainView)
        {
            _curtainView = curtainView;
            _curtainView.Init(_uiConfig.CurtainAnimDuration);
        }

        public void ShowCurtain(bool isAnimated = true, Action callback = null)
            => _curtainView.ShowCurtain(isAnimated, callback);

        public void HideCurtain(bool isAnimated = true, Action callback = null)
            => _curtainView.HideCurtain(isAnimated, callback);

        public void HideCurtain(float startDelay, Action callback = null)
            => _curtainView.HideCurtain(startDelay, callback);
    }
}