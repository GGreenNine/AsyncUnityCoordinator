using System.Threading;
using Cysharp.Threading.Tasks;

namespace Grigorii.Tatarinov.UnityCoordinator
{
    public interface IAsyncRouter
    {
        UniTask<IRouteResult> TransitionAsync(ICoordinator parent, ICoordinator current, ICoordinator next, CancellationToken cancellationToken);

        UniTask<IRouteResult> TransitionModallyAsync(ICoordinator current, ICoordinator next, CancellationToken cancellationToken);
    }
}