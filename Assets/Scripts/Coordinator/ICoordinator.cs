using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;

namespace Grigorii.Tatarinov.UnityCoordinator
{
    public interface ICoordinator
    {
        UniTask<IRouteResult> Present(ICoordinator parent, CancellationToken cancellationToken);
        void Dismiss();
        void AddChild(ICoordinator coordinator);
        void RemoveChild(ICoordinator coordinator);
        ICoordinator Parent { get; }
        List<ICoordinator> Children { get; }
    }
}