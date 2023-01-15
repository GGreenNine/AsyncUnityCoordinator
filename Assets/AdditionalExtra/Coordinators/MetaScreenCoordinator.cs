using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Zenject;

namespace Grigorii.Tatarinov.UnityCoordinator
{
    public class MetaScreenCoordinator : AbstractCoordinator, IInitializable
    {
        private readonly IFactory<MetaScreenPresenter> _loadingPresenterFactory;
        private readonly FullScreenPopupHolder _canvas;
        private readonly IAsyncRouter _router;

        private MetaScreenPresenter _metaScreenPresenter;

        public MetaScreenCoordinator(IFactory<MetaScreenPresenter> loadingPresenterFactory, FullScreenPopupHolder canvas, IAsyncRouter router)
        {
            _loadingPresenterFactory = loadingPresenterFactory;
            _canvas = canvas;
            _router = router;
        }
        
        public void Initialize()
        {
            _router.Transition(null, null, this, OnDismissTokenSource.Token);
        }
        
        protected override UniTask<IRouteResult> OnPresent(CancellationToken token)
        {
            using (var cts = CancellationTokenSource.CreateLinkedTokenSource(token))
            {
                _metaScreenPresenter = _loadingPresenterFactory.Create();
                _metaScreenPresenter.transform.SetParent(_canvas.transform, false);
                return new UniTask<IRouteResult>(new RouteSuccessResult());
            }
        }
    }
}