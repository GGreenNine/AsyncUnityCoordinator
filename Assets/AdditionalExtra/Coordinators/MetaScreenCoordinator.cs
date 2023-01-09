using System.Threading;
using Cysharp.Threading.Tasks;
using Zenject;

namespace Grigorii.Tatarinov.UnityCoordinator
{
    public class MetaScreenCoordinator : AbstractCoordinator
    {
        private readonly IFactory<MetaScreenPresenter> _loadingPresenterFactory;
        private readonly FullScreenPopupHolder _canvas;

        private MetaScreenPresenter _metaScreenPresenter;

        public MetaScreenCoordinator(IFactory<MetaScreenPresenter> loadingPresenterFactory, FullScreenPopupHolder canvas)
        {
            _loadingPresenterFactory = loadingPresenterFactory;
            _canvas = canvas;
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