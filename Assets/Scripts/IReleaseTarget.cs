namespace Grigorii.Tatarinov.UnityCoordinator
{
    public interface IReleaseTarget
    {
        IReleaseStrategy ReleaseStrategy { get; }
    }
}