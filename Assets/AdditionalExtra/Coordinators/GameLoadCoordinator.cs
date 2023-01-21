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
        private readonly IFactory<MetaSceneLoadCoordinator> _metaSceneLoadingCoordFactory;

        public GameLoadCoordinator(IAsyncRouter router, IFactory<LoadingCoordinator> loadingCoordinatorFactory, IFactory<MetaSceneLoadCoordinator> metaSceneLoadingCoordFactory)
        {
            _router = router;
            _loadingCoordinatorFactory = loadingCoordinatorFactory;
            _metaSceneLoadingCoordFactory = metaSceneLoadingCoordFactory;
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
            await _router.TransitionAsync(this, null, _loadingCoordinator, token);
        }

        private async UniTask LoadMetaScreen(CancellationToken token)
        {
            var metaSceneCoord = _metaSceneLoadingCoordFactory.Create();
            await _router.TransitionAsync(this, _loadingCoordinator, metaSceneCoord, token);
        }
        
    }
}