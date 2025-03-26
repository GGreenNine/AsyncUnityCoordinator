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
        private readonly FullScreenPopupHolder _canvas;
        
        private IMonoModule<EmptyViewModel> _loadingViewPresenter;

        public LoadingCoordinator(IFactory<IMonoModule<EmptyViewModel>> loadingPresenterFactory, IFactory<ConfirmationCoordinator> coordinatorFactory, IAsyncRouter router,
            FullScreenPopupHolder canvas)
        {
            _canvas = canvas;
            _loadingPresenterFactory = loadingPresenterFactory;
            _coordinatorFactory = coordinatorFactory;
            _router = router;
        }

        protected override async UniTask<IRouteResult> OnPresent(CancellationToken ct)
        {
            using (var cts = CancellationTokenSource.CreateLinkedTokenSource(ct))
            {
                _loadingViewPresenter = _loadingPresenterFactory.Create(); // creating a new loading presenter using factory
                _loadingViewPresenter.Self.SetParent(_canvas.transform, false);
            
                await UniTask.Delay(3000, cancellationToken: cts.Token); // imitate loading process
                await Confirmation(cts.Token); // waiting for confirmation coordinator result
                return new RouteSuccessResult();
            }
        }

        protected override void OnDismiss()
        {
            base.OnDismiss();
            _loadingViewPresenter.ReleaseStrategy?.Release();
        }

        private async UniTask Confirmation(CancellationToken ct)
        {
            var confirmationCoord = _coordinatorFactory.Create(); // creating a new confirmation coordinator using factory
            var result = await _router.TransitionModallyAsync(this, confirmationCoord, ct); // waiting for player confirmation
            if (result is RouteFailedResult) // if player declined the confirmation we close the app 
            {
#if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
#else
         Application.Quit();
#endif
            }
        }
    }
}