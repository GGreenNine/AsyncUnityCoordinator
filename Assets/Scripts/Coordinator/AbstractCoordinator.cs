using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Cysharp.Threading.Tasks;

namespace Grigorii.Tatarinov.UnityCoordinator
{
    public class AbstractCoordinator : ICoordinator
    {
        private CancellationTokenSource _onDismissTokenSource = new CancellationTokenSource();
        private readonly HashSet<ICoordinator> _children = new HashSet<ICoordinator>();
        protected ICoordinator Parent;
        
        public async UniTask<IRouteResult> Present(ICoordinator parent, CancellationToken cancellationToken)
        {
            Parent = parent;
            using var combinedTokenSource = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken, _onDismissTokenSource.Token);
            return await OnPresent(combinedTokenSource.Token);
        }

        public void Dismiss()
        {
            foreach (var coordinator in _children.ToList())
            {
                coordinator.Dismiss();
            }
            
            _children.Clear();
            
            OnDismiss();
        }

        public void AddChild(ICoordinator coordinator)
        {
            _children.Add(coordinator);
        }

        public void RemoveChild(ICoordinator coordinator)
        {
            _children.Remove(coordinator);
        }

        protected virtual UniTask<IRouteResult> OnPresent(CancellationToken cancellationToken)
        {
            return new UniTask<IRouteResult>(new RouteSuccessResult());
        }

        protected virtual void OnDismiss()
        {
            _onDismissTokenSource?.Cancel();
            _onDismissTokenSource?.Dispose();
            _onDismissTokenSource = new CancellationTokenSource();
        }
    }
}