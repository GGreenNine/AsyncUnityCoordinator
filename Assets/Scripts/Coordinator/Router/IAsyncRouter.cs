using System.Threading;
using Cysharp.Threading.Tasks;

namespace Grigorii.Tatarinov.UnityCoordinator
{
    public interface IAsyncRouter
    {
        UniTask<IRouteResult> Transition(ICoordinator parent, ICoordinator current, ICoordinator next, CancellationToken cancellationToken);

        UniTask<IRouteResult> TransitionModally(ICoordinator current, ICoordinator next, CancellationToken cancellationToken);
    }
}