using System.Collections.Generic;
using DG.Tweening;

namespace Infrastructure
{
    internal sealed class Cleaner : ICleaner
    {
        private readonly List<ICleanable> _cleanables = new();
        

        public void AddCleanable(ICleanable cleanable)
        {
            _cleanables.Add(cleanable);
        }

        public void RemoveCleanable(ICleanable cleanable)
        {
            _cleanables.Remove(cleanable);
        }

        public void CleanUp()
        {
            foreach (var cleanable in _cleanables)
                cleanable.CleanUp();
            _cleanables.Clear();
            DOTween.Clear();
        }
    }
}