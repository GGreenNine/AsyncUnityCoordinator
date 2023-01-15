using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Cysharp.Threading.Tasks;

namespace Grigorii.Tatarinov.UnityCoordinator
{
    public class AbstractCoordinator : ICoordinator, IDisposable
    {
        protected CancellationTokenSource OnDismissTokenSource = new();
        private readonly HashSet<ICoordinator> _children = new();
        public ICoordinator Parent { get; private set; }
        public List<ICoordinator> Children => _children.ToList();

        private bool _isDismissed = false;

        public async UniTask<IRouteResult> Present(ICoordinator parent, CancellationToken cancellationToken)
        {
            Parent = parent;
            using var combinedTokenSource = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken, OnDismissTokenSource.Token);
            return await OnPresent(combinedTokenSource.Token);
        }

        public void Dismiss()
        {
            if (_isDismissed) return;
            
            foreach (var coordinator in _children.ToList())
            {
                coordinator.Dismiss();
            }
            
            Parent?.RemoveChild(this);
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
            CoordinatorTracker.StopTracking(this);
            OnDismissTokenSource?.Cancel();
            OnDismissTokenSource?.Dispose();
            OnDismissTokenSource = new CancellationTokenSource();
            _isDismissed = true;
        }

        public virtual void Dispose()
        {
            OnDismiss();
        }
    }
}