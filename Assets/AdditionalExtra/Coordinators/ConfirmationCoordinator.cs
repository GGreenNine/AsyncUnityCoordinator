using System.Threading;
using Cysharp.Threading.Tasks;
using Zenject;

namespace Grigorii.Tatarinov.UnityCoordinator
{
    public class ConfirmationCoordinator : AbstractCoordinator
    {
        private readonly IFactory<ConfirmationViewPresenter> _confirmationPopupFactory;
        private readonly PopupHolder _canvas;

        private ConfirmationViewPresenter _presenter;

        public ConfirmationCoordinator(IFactory<ConfirmationViewPresenter> confirmationPopupFactory, PopupHolder canvas)
        {
            _confirmationPopupFactory = confirmationPopupFactory;
            _canvas = canvas;
        }

        protected override async UniTask<IRouteResult> OnPresent(CancellationToken ct)
        {
            var source = CancellationTokenSource.CreateLinkedTokenSource(ct);
            using (source)
            {
                return await Show(source.Token);
            }
        }

        protected override void OnDismiss()
        {
            if (_presenter != null)
            {
                _presenter.ReleaseStrategy.Release();
            }
        }

        private async UniTask<IRouteResult> Show(CancellationToken token)
        {
            _presenter = _confirmationPopupFactory.Create();
            _presenter.transform.SetParent(_canvas.transform, false);
            var result = await UniTask.WhenAny(_presenter.OnAcceptClick(token), _presenter.OnDismissClick(token));
            return result switch
            {
                0 => new RouteSuccessResult(),
                _ => new RouteFailedResult()
            };
        }
    }
}