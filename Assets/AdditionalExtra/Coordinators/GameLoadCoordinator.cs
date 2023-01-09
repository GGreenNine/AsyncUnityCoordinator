using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Zenject;

namespace Grigorii.Tatarinov.UnityCoordinator
{
    public class GameLoadCoordinator : AbstractCoordinator
    {
        private LoadingCoordinator _loadingCoordinator;

        private readonly IAsyncRouter _router;
        private readonly IFactory<LoadingCoordinator> _loadingCoordinatorFactory;
        private readonly IFactory<MetaScreenCoordinator> _metaScreenCoordFactory;

        public GameLoadCoordinator(IAsyncRouter router, IFactory<LoadingCoordinator> loadingCoordinatorFactory, IFactory<MetaScreenCoordinator> metaScreenCoordFactory)
        {
            _router = router;
            _loadingCoordinatorFactory = loadingCoordinatorFactory;
            _metaScreenCoordFactory = metaScreenCoordFactory;
        }

        protected override async UniTask<IRouteResult> OnPresent(CancellationToken token)
        {
            using (var s = CancellationTokenSource.CreateLinkedTokenSource(token))
            {
                await Loading(s.Token);
                await LoadMetaScreen(s.Token);
                return new RouteSuccessResult();
            }
        }

        protected override void OnDismiss()
        {
            base.OnDismiss();
            _loadingCoordinator = null;
        }

        private async UniTask Loading(CancellationToken token)
        {
            _loadingCoordinator = _loadingCoordinatorFactory.Create();
            await _router.Transition(this, null, _loadingCoordinator, token);
        }

        private async UniTask LoadMetaScreen(CancellationToken token)
        {
            var metaScreenCoord = _metaScreenCoordFactory.Create();
            await _router.Transition(this, _loadingCoordinator, metaScreenCoord, token);
        }
        
    }
}