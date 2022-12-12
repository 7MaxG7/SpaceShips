namespace Abstractions.Ships
{
    internal interface IDowngradable<out T>
    {
        T Downgrade(IModule module);
    }
}