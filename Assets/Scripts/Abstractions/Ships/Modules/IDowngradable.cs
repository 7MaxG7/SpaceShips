namespace Abstractions.Ships
{
    public interface IDowngradable<out T>
    {
        T Downgrade(IModule module);
    }
}