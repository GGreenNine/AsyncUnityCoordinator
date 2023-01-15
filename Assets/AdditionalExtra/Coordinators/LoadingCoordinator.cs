using System.Threading;
using Cysharp.Threading.Tasks;
using Grigorii.Tatarinov.UnityCoordinator.ViewModels;
using UnityEngine;
using Zenject;

namespace Grigorii.Tatarinov.UnityCoordinator
{
    public class LoadingCoordinator : AbstractCoordinator
    {
        private readonly IAsyncRouter _router;
        private readonly IFactory<ConfirmationCoordinator> _coordinatorFactory;
        private readonly IFactory<IMonoModule<EmptyViewModel>> _loadingPresenterFactory;
        private readonly IFactory<LoadingCoordinator> _loadingCoordinatorFactory;
        private readonly FullScreenPopupHolder _canvas;
        
        private IMonoModule<EmptyViewModel> _loadingViewPresenter;

        public LoadingCoordinator(IFactory<IMonoModule<EmptyViewModel>> loadingPresenterFactory, IFactory<ConfirmationCoordinator> coordinatorFactory, IAsyncRouter router,
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
            using (var cts = CancellationTokenSource.CreateLinkedTokenSource(ct))
            {
                _loadingViewPresenter = _loadingPresenterFactory.Create();
                _loadingViewPresenter.Self.SetParent(_canvas.transform, false);
            
                await StartTimer(cts.Token);
                return new RouteSuccessResult();
            }
        }

        protected override void OnDismiss()
        {
            base.OnDismiss();
            _loadingViewPresenter.ReleaseStrategy?.Release();
        }

        private async UniTask StartTimer(CancellationToken ct)
        {
            await UniTask.Delay(3000, cancellationToken: ct);

            var confirmationCoord = _coordinatorFactory.Create();
            var result = await _router.TransitionModally(this, confirmationCoord, ct);
            if (result is RouteFailedResult)
            {
                /*
                 *                     confirmationCoord.Dismiss();
                    _loadingViewPresenter.ReleaseStrategy?.Release();
                    
                    var loadingCoord = _loadingCoordinatorFactory.Create();
                    await _router.TransitionModally(this, loadingCoord, ct);
                 */
#if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
#else
         Application.Quit();
#endif
            }
        }
    }
}