namespace Infrastructure
{
    internal interface ICleaner : ICleanable
    {
        void AddCleanable(ICleanable cleanable);
        void RemoveCleanable(ICleanable cleanable);
    }
}