using System.Collections;
using System.Threading;
using Cysharp.Threading.Tasks;

namespace Grigorii.Tatarinov.UnityCoordinator
{
    public class AsyncRouter : IAsyncRouter
    {
        public async UniTask<IRouteResult> TransitionAsync(ICoordinator parent, ICoordinator current, ICoordinator next, CancellationToken cancellationToken)
        {
            current?.Dismiss();
            parent?.AddChild(next);
            
            var presentTask = next.Present(parent, cancellationToken);
            CoordinatorTracker.TrackCoordinator(next);
            var routeResult = await presentTask;
            
            return routeResult;
        }
        
        public async UniTask<IRouteResult> TransitionModallyAsync(ICoordinator current, ICoordinator next, CancellationToken cancellationToken)
        {
            current?.AddChild(next);

            var presentTask = next.Present(current, cancellationToken);
            CoordinatorTracker.TrackCoordinator(next);
            var routeResult = await presentTask;
            
            next.Dismiss();
            return routeResult;
        }

    }
}