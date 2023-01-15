using System.Threading;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Linq;
using Grigorii.Tatarinov.UnityCoordinator.ViewModels;
using Zenject;

namespace Grigorii.Tatarinov.UnityCoordinator
{
    public class ConfirmationCoordinator : AbstractCoordinator
    {
        private readonly IFactory<IMonoModule<ConfirmationViewModel>> _confirmationPopupFactory;
        private readonly PopupHolder _canvas;

        private IMonoModule<ConfirmationViewModel> _presenter;

        public ConfirmationCoordinator(IFactory<IMonoModule<ConfirmationViewModel>> confirmationPopupFactory, PopupHolder canvas)
        {
            _confirmationPopupFactory = confirmationPopupFactory;
            _canvas = canvas;
        }

        protected override UniTask<IRouteResult> OnPresent(CancellationToken ct)
        {
            var source = CancellationTokenSource.CreateLinkedTokenSource(ct);
            using (source)
            {
                return Show(source.Token);
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
            _presenter.Self.SetParent(_canvas.transform, false);
            var model = new ConfirmationViewModel();
            _presenter.SetModel(model);
            
            var acceptClick = model.OnAcceptClick.FirstAsync(token);
            var dismissClick = model.OnDismissClick.FirstAsync(token);
            
            var result = await UniTask.WhenAny(acceptClick, dismissClick);
 
            return result.winArgumentIndex switch
            {
                0 => new RouteSuccessResult(),
                _ => new RouteFailedResult()
            };
        }
    }
}