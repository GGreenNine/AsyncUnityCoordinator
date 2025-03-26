using UnityEngine;
using Zenject;

namespace Grigorii.Tatarinov.UnityCoordinator
{
    public class MetaSceneContextInstaller : MonoInstaller
    {
        [SerializeField] private MetaScreenPresenter _metaScreenPresenter;

        public override void InstallBindings()
        {
            Container.BindInterfacesTo<MetaScreenCoordinator>().AsSingle();
            
            Container.BindIFactory<MetaScreenPresenter>().FromMethod(container =>
            {
                return Container.InstantiatePrefabForComponent<MetaScreenPresenter>(_metaScreenPresenter);
            });
        }
    }
}