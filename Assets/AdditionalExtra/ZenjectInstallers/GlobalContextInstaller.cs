using UnityEngine;
using Zenject;

namespace Grigorii.Tatarinov.UnityCoordinator
{
    public class GlobalContextInstaller : MonoInstaller
    {
        [SerializeField] private LoadingViewPresenter _loadingViewPresenter;
        [SerializeField] private ConfirmationViewPresenter _confirmationViewPresenter;
        [SerializeField] private MetaScreenPresenter _metaScreenPresenter;
        [SerializeField] private FullScreenPopupHolder _mainCanvas;
        [SerializeField] private PopupHolder _popupHolder;
        
        public override void InstallBindings()
        {
            BindCoordinators();
            BindFactories();
            
            Container.BindInterfacesAndSelfTo<GameEntryPoint>().AsSingle();
            Container.Bind<FullScreenPopupHolder>().FromInstance(_mainCanvas).AsSingle();
            Container.Bind<PopupHolder>().FromInstance(_popupHolder).AsSingle();
            Container.BindInterfacesTo<AsyncRouter>().AsSingle();
        }

        private void BindFactories()
        {
            Container.BindIFactory<LoadingViewPresenter>().FromMethod(container =>
            {
                return Container.InstantiatePrefabForComponent<LoadingViewPresenter>(_loadingViewPresenter);
            });
            
            Container.BindIFactory<ConfirmationViewPresenter>().FromMethod(container =>
            {
                return Container.InstantiatePrefabForComponent<ConfirmationViewPresenter>(_confirmationViewPresenter);
            });
            
            Container.BindIFactory<MetaScreenPresenter>().FromMethod(container =>
            {
                return Container.InstantiatePrefabForComponent<MetaScreenPresenter>(_metaScreenPresenter);
            });
        }

        private void BindCoordinators()
        {
            BindCoordinator<GameLoadCoordinator>();
            BindCoordinator<LoadingCoordinator>();
            BindCoordinator<ConfirmationCoordinator>();
            BindCoordinator<MetaScreenCoordinator>();
        }

        private void BindCoordinator<T>() where T : ICoordinator
        {
            Container.BindIFactory<T>().FromMethod(container => (T) CreateCoordinator<T>(container));
        }

        private ICoordinator CreateCoordinator<T>(IInstantiator container) where T : ICoordinator
        {
            return container.Instantiate<T>();
        }
    }
}