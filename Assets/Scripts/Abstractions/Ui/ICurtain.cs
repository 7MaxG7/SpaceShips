using System;
using Ui;

namespace Abstractions.Services
{
    internal interface ICurtain
    {
        void Prepare(CurtainView curtainView);
        void ShowCurtain(bool isAnimated = true, Action callback = null);
        void HideCurtain(bool isAnimated = true, Action callback = null);
        void HideCurtain(float startDelay, Action callback = null);
    }
}