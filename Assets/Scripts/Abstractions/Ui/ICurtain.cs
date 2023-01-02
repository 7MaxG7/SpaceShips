using System;
using System.Threading.Tasks;

namespace Abstractions.Services
{
    public interface ICurtain
    {
        Task InitAsync();
        void ShowCurtain(bool isAnimated = true, Action callback = null);
        void HideCurtain(bool isAnimated = true, Action callback = null);
        void HideCurtain(float startDelay, Action callback = null);
    }
}