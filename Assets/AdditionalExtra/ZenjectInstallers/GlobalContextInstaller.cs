using Grigorii.Tatarinov.UnityCoordinator.ViewModels;
using UnityEngine;
using Zenject;

namespace Grigorii.Tatarinov.UnityCoordinator
{
    public class GlobalContextInstaller : MonoInstaller
    {
        [SerializeField] private LoadingViewPresenter _loadingViewPresenter;
        [SerializeField] private ConfirmationViewPresenter _confirmationViewPresenter;
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
            Container.BindIFactory<IMonoModule<EmptyViewModel>>().FromMethod(_ =>
            {
                return Container.InstantiatePrefabForComponent<LoadingViewPresenter>(_loadingViewPresenter);
            });
            
            Container.BindIFactory<IMonoModule<ConfirmationViewModel>>().FromMethod(_ =>
            {
                return Container.InstantiatePrefabForComponent<ConfirmationViewPresenter>(_confirmationViewPresenter);
            });
            

        }

        private void BindCoordinators()
        {
            Container.BindCoordinatorFactory<GameLoadCoordinator>();
            Container.BindCoordinatorFactory<LoadingCoordinator>();
            Container.BindCoordinatorFactory<ConfirmationCoordinator>();
            Container.BindCoordinatorFactory<MetaSceneLoadCoordinator>();
            
        }
    }
}