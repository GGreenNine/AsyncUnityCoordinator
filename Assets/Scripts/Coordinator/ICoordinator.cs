using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;

namespace Grigorii.Tatarinov.UnityCoordinator
{
// An interface that defines the basic methods and properties needed to implement the Coordinator pattern
    public interface ICoordinator
    {
        // Presents the current coordinator and returns a UniTask<IRouteResult> that completes when the presentation is finished
        UniTask<IRouteResult> Present(ICoordinator parent, CancellationToken cancellationToken);
        // Dismiss the current coordinator and all of its children coordinators
        void Dismiss();
        // Add a child coordinator to the current coordinator
        void AddChild(ICoordinator coordinator);
        // Removes a child coordinator from the current coordinator
        void RemoveChild(ICoordinator coordinator);
        // The parent coordinator of the current coordinator
        ICoordinator Parent { get; }
        // The list of child coordinators of the current coordinator
        List<ICoordinator> Children { get; }
    }
}