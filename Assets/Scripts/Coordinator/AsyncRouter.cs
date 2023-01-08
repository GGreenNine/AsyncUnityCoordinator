using System.Threading;
using Cysharp.Threading.Tasks;

namespace Grigorii.Tatarinov.UnityCoordinator
{
    public interface IRouteResult { }

    public class RouteSuccessResult : IRouteResult{}
    public class RouteFailedResult : IRouteResult{}
    public class AsyncRouter : IAsyncRouter
    {
        public async UniTask<IRouteResult> Transition(ICoordinator parent, ICoordinator current, ICoordinator next, CancellationToken cancellationToken)
        {
            current?.Dismiss();
            parent.RemoveChild(current);
            var routeResult = await next.Present(parent, cancellationToken);
            parent.AddChild(next);
            return routeResult;
        }
    }
}