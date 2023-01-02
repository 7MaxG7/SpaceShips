using System;
using System.Threading.Tasks;
using Abstractions.Services;
using Ui;
using Zenject;

namespace Services
{
    public sealed class Curtain : ICurtain
    {
        private readonly IUiFactory _uiFactory;

        private CurtainView _curtainView;

        
        [Inject]
        public Curtain(IUiFactory uiFactory)
        {
            _uiFactory = uiFactory;
        }

        public async Task InitAsync()
        {
            _curtainView = await _uiFactory.CreateCurtainAsync();
        }

        public void ShowCurtain(bool isAnimated = true, Action callback = null)
            => _curtainView.ShowCurtain(isAnimated, callback);

        public void HideCurtain(bool isAnimated = true, Action callback = null)
            => _curtainView.HideCurtain(isAnimated, callback);

        public void HideCurtain(float startDelay, Action callback = null)
            => _curtainView.HideCurtain(startDelay, callback);
    }
}