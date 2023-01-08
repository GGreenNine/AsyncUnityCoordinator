using System.Threading;
using Cysharp.Threading.Tasks;
using Zenject;

namespace Grigorii.Tatarinov.UnityCoordinator
{
    public class LoadingCoordinator : AbstractCoordinator
    {
        private readonly IAsyncRouter _router;
        private readonly IFactory<ConfirmationCoordinator> _coordinatorFactory;
        private readonly IFactory<LoadingViewPresenter> _loadingPresenterFactory;
        private readonly IFactory<LoadingCoordinator> _loadingCoordinatorFactory;
        private readonly FullScreenPopupHolder _canvas;
        
        private LoadingViewPresenter _loadingViewPresenter;

        public LoadingCoordinator(IFactory<LoadingViewPresenter> loadingPresenterFactory, IFactory<ConfirmationCoordinator> coordinatorFactory, IAsyncRouter router,
            FullScreenPopupHolder canvas, IFactory<LoadingCoordinator> loadingCoordinatorFactory)
        {
            _loadingCoordinatorFactory = loadingCoordinatorFactory;
            _canvas = canvas;
            _loadingPresenterFactory = loadingPresenterFactory;
            _coordinatorFactory = coordinatorFactory;
            _router = router;
        }

        protected override async UniTask<IRouteResult> OnPresent(CancellationToken ct)
        {
            using (CancellationTokenSource.CreateLinkedTokenSource(ct))
            {
                _loadingViewPresenter = _loadingPresenterFactory.Create();
                _loadingViewPresenter.transform.SetParent(_canvas.transform, false);
            
                await StartTimer(ct);
                return new RouteSuccessResult();
            }
        }

        protected override void OnDismiss()
        {
            base.OnDismiss();
            _loadingViewPresenter.ReleaseStrategy.Release();
        }

        private async UniTask StartTimer(CancellationToken ct)
        {
            await UniTask.Delay(3000, cancellationToken: ct);

            var confirmationCoord = _coordinatorFactory.Create();
            var result = await _router.Transition(this, null, confirmationCoord, ct);
            if (result is RouteFailedResult)
            {
                var loadingCoord = _loadingCoordinatorFactory.Create();
                await _router.Transition(this, confirmationCoord, loadingCoord, ct);
            }
        }
    }
}